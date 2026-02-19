import os
import datetime

import json
import requests
from urllib.parse import urljoin
import logging

logging.basicConfig(
    filename="app.log",
    level=logging.INFO,
    format="%(asctime)s [%(levelname)s] %(filename)s:%(lineno)d %(funcName)s() - %(message)s"
)


logging.info("Application started")
logging.error("Something went wrong")
logging.critical("Fatal error occurred")

class OpenWebUI:
    config_file = "config.json"
    base_url = ""
    api_key = ""
    openwebui_foldername = ""
    folder_id = ""
    question = ""

    def get_settings(self):
        """Load and return settings from config.json."""
        try:
            with open(self.config_file, "r") as f:
                return json.load(f)
        except FileNotFoundError:
            logging.error(f"Error: {self.config_file} not found.")
            return {}
        except json.JSONDecodeError:
            logging.error(f"Error: {self.config_file} contains invalid JSON.")
            return {}

    def __init__(self):
        settings = self.get_settings()
        self.base_url = settings.get("base_url", "")
        self.api_key = settings.get("api_key", "")
        self.openwebui_foldername = settings.get("openwebui_foldername", "Jobs")
        self.upload_folder = settings.get("upload_folder", "uploads")
        self.question = settings.get("question", "Based on Resume.pdf, am I a good candidate?return {result:yes/no, reason:...,missing: missing abilities}. otherwise create a cover letter.")

    def upload_file(self, filename):
        """Upload a file to the specified API endpoint.

        Args:
            filename (str): The name of the file to be uploaded.

        Returns:
            dict or None: A dictionary containing the API response if successful,
                        otherwise None.
        """

        self.get_folders()

        endpoint = "/api/v1/files/?process=true&process_in_background=true"
        full_url = urljoin(self.base_url, endpoint)
        headers = {"Authorization": f"Bearer {self.api_key}"}
        files = {"file": open(os.path.join(self.upload_folder, filename), "rb")}
        data = {"folder_id": self.folder_id}

        try:
            response = requests.post(
                full_url, headers=headers, files=files, data=data, timeout=10
            )

            if response.status_code == 200:
                file_id = response.json().get("id")
                print(f"File uploaded successfully. File ID: {file_id}")

                new_chat_response = self.create_new_chat(file_id, filename)

                chat_id = new_chat_response.get("id")
                print(f"New chat created successfully. Chat ID: {chat_id}")

                get_completions_response = self.get_completions(chat_id, file_id)
                self.save_assistant(chat_id, get_completions_response.get("content", ""))
                
                return get_completions_response

            elif response.status_code == 401:
                logging.info("Error: Unauthorized - Invalid API key.")
                return None
            elif response.status_code == 404:
                logging.info("Error: The requested resource was not found.")
                return None
            else:
                logging.error(f"Unexpected error occurred. Status code: {response.status_code}")
                logging.error(f"Response text: {response.text}")
                return None
        except requests.exceptions.Timeout:
            logging.error("Request timed out after 10 seconds.")
            return None
        except requests.exceptions.RequestException as e:
            logging.error(f"An error occurred during the request: {str(e)}")
            return None

    def get_completions(self, chat_id, file_id):
        endpoint = "/api/chat/completions"
        full_url = urljoin(self.base_url, endpoint)
        headers = {"Authorization": f"Bearer {self.api_key}"}
        data = {
            "chat_id": chat_id,
            "model": "gpt-oss:20b",
            "messages": [
                {
                    "role": "user",
                    "content": self.question,
                    "files": ["file-" + file_id],
                },
            ],
            "stream": False,
        }
        
        logging.info(f"Start get completions. Sending request to {full_url} with data: {data}")
        #return  # specifically returns for debugging the get_completions function without making the actual API call

        try:
            logging.info(f"Sending request to {full_url} with data: {data}")            
            response = requests.post(full_url, headers=headers, json=data, timeout=300)
            logging.info(f"Get completions response : {response}")
            if response.status_code == 200:
                content = response.json()["choices"][0]["message"]["content"]
                logging.info(f"Content retrieved: {content}")
                clean = content.strip().strip("```").replace("json", "", 1).strip()
                parsed = json.loads(clean)
                return parsed
            elif response.status_code == 401:
                logging.error("Error: Unauthorized - Invalid API key.")
                return None
            elif response.status_code == 404:
                logging.error("Error: The requested resource was not found.")
                return None
            else:
                logging.error(f"Unexpected error occurred. Status code: {response.status_code}")
                logging.error(f"Response text: {response.text}")
                return None
        except requests.exceptions.Timeout:
            logging.error("Request timed out after 10 seconds.")
            return None

    def get_folders(self):
        """Fetch folders from the specified API endpoint.

        Args:
            base_url (str): The base URL of the API.
            api_key (str): The API key for authentication.

        Returns:
            dict or None: A dictionary containing the API response if successful,
                        otherwise None.

            [
            {
                "created_at": 1770896628,
                "id": "54758bfe-523f-49f5-b5ff-7232a5fdb72e",
                "is_expanded": false,
                "meta": {
                "icon": null
                },
                "name": "Amir",
                "parent_id": null,
                "updated_at": 1771349041
            },
            {
                "created_at": 1771338919,
                "id": "f90a9d11-b790-4484-b893-597dc7f4059b",
                "is_expanded": true,
                "meta": null,
                "name": "Jobs",
                "parent_id": null,
                "updated_at": 1771349041
            }
            ]
        """
        endpoint = "/api/v1/folders/"
        full_url = urljoin(self.base_url, endpoint)
        headers = {"Authorization": f"Bearer {self.api_key}"}

        try:
            response = requests.get(full_url, headers=headers, timeout=10)

            if response.status_code == 200:
                # get id of the first folder with the name "Jobs"
                folders = response.json()
                for folder in folders:
                    if folder["name"] == self.openwebui_foldername:
                        self.folder_id = folder["id"]
                        break
                logging.info("getting folders...")
                logging.info(f"Folders: {folders}")
                logging.info(f"Selected folder ID: {self.folder_id}")
                return response.json()
            elif response.status_code == 401:
                logging.error("Error: Unauthorized - Invalid API key.")
                return None
            elif response.status_code == 404:
                logging.error("Error: The requested resource was not found.")
                return None
            else:
                logging.error(f"Unexpected error occurred. Status code: {response.status_code}")
                logging.error(f"Response text: {response.text}")
                return None
        except requests.exceptions.Timeout:
            logging.error("Request timed out after 10 seconds.")
            return None
        except requests.exceptions.RequestException as e:
            logging.error(f"An error occurred during the request: {str(e)}")
            return None

    def create_new_chat(self, file_id, filename):
        endpoint = "/api/v1/chats/new"
        full_url = urljoin(self.base_url, endpoint)
        headers = {"Authorization": f"Bearer {self.api_key}"}

        
        data = {"chat": {
            "title": filename,
            "message": self.question,
            "files": ["file-" + file_id]
            }   ,
            "model": "gpt-oss:20b", 
            "folder_id": self.folder_id 
            
    }
        logging.info(f"Creating new chat with data: {data} at {full_url}")
        
        try:
            response = requests.post(full_url, headers=headers, json=data, timeout=10)

            if response.status_code == 200:
                return response.json()
            elif response.status_code == 401:
                logging.error("Error: Unauthorized - Invalid API key.")
                return None
            elif response.status_code == 404:
                logging.error("Error: The requested resource was not found.")
                return None
            else:
                logging.error(f"Unexpected error occurred. Status code: {response.status_code}")
                logging.error(f"Response text: {response.text}")
                return None
        except requests.exceptions.Timeout:
            logging.error("Request timed out after 10 seconds.")
            return None
        except requests.exceptions.RequestException as e:
            logging.error(f"An error occurred during the request: {str(e)}")
            return None


    def save_assistant(self, chat_id, content):
        """
        Save the assistant's reply into the chat so it appears in the Open-WebUI GUI.
        """
        endpoint = f"/api/chat/message"
        full_url = urljoin(self.base_url, endpoint)
        headers = {"Authorization": f"Bearer {self.api_key}"}

        data = {
            "role": "assistant",
            "content": content
        }

        logging.info(f"Saving assistant message to chat {chat_id}: {content[:80]}...")

        try:
            response = requests.post(full_url, headers=headers, json=data, timeout=10)
            response.raise_for_status()
            logging.info("Assistant message saved successfully.")
            return response.json()

        except requests.exceptions.Timeout:
            logging.error("Timeout while saving assistant message.")
            return None

        except requests.exceptions.RequestException as e:
            logging.error(f"Error saving assistant message: {str(e)}")
            return None