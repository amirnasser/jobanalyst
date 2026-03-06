import os
import json
import requests
from urllib.parse import urljoin
import logging

logging.basicConfig(
    filename="app.log",
    level=logging.INFO,
    format="%(asctime)s [%(levelname)s] %(filename)s:%(lineno)d %(funcName)s() - %(message)s",
)

class OpenWebUI:
    config_file = "config.json"
    base_url = ""
    api_key = ""
    openwebui_foldername = ""
    folder_id = ""
    question = ""
    chat_id = ""
    upload_filename = ""

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
        
        # read the file propmpt.txt and set it as the question
        try:
            with open("prompt.txt", "r") as f:
                self.question = f.read().strip()
        except FileNotFoundError:
            logging.error("Error: prompt.txt not found. Using default question.")
            self.question = "What is in the file?"

        # self.question = settings.get(
        #     "question",
        #     self.question,
        # )

    def upload_file(self, filename):
        """Upload a file to the specified API endpoint.

        Args:
            filename (str): The name of the file to be uploaded.

        Returns:
            dict or None: A dictionary containing the API response if successful,
                        otherwise None.
        """

        self.get_folders()
        self.upload_filename = filename
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
                return response.json()

            elif response.status_code == 401:
                logging.info("Error: Unauthorized - Invalid API key.")
                return None
            elif response.status_code == 404:
                logging.info("Error: The requested resource was not found.")
                return None
            else:
                logging.error(
                    f"Unexpected error occurred. Status code: {response.status_code}"
                )
                logging.error(f"Response text: {response.text}")
                return None
        except requests.exceptions.Timeout:
            logging.error("Request timed out after 10 seconds.")
            return None
        except requests.exceptions.RequestException as e:
            logging.error(f"An error occurred during the request: {str(e)}")
            return None

    def delete_chat(self):
        endpoint = f"/api/v1/chats/{self.chat_id}"
        full_url = urljoin(self.base_url, endpoint)
        headers = {"Authorization": f"Bearer {self.api_key}"}

        try:
            response = requests.delete(full_url, headers=headers, timeout=10)

            if response.status_code == 200:
                logging.info(f"Chat {self.chat_id} deleted successfully.")
                self.chat_id = None
                return True
            elif response.status_code == 401:
                logging.error("Error: Unauthorized - Invalid API key.")
                return False
            elif response.status_code == 404:
                logging.error("Error: The requested resource was not found.")
                return False
            else:
                logging.error(
                    f"Unexpected error occurred. Status code: {response.status_code}"
                )
                logging.error(f"Response text: {response.text}")
                return False
        except requests.exceptions.Timeout:
            logging.error("Request timed out after 10 seconds.")
            return False
        except requests.exceptions.RequestException as e:
            logging.error(f"An error occurred during the request: {str(e)}")
            return False

    def get_completions(self, file_id):
        endpoint = "/api/chat/completions"
        full_url = urljoin(self.base_url, endpoint)
        headers = {"Authorization": f"Bearer {self.api_key}"}

        data = {
            "chat_id": self.chat_id,
            "model": "gpt-oss:20b",
            "messages": [
                {
                    "role": "user",
                    "content": self.question,
                    "files":["file-" + file_id],
                }
            ],
            "stream": False
        }

        logging.info(
            f"Start get completions. Sending request to {full_url} with data: {data}"
        )

        try:
            logging.info(
                f"Sending request for completions to {full_url} with data: {data}"
            )
            response = requests.post(full_url, headers=headers, json=data, timeout=300)
            logging.info(f"Get completions response : {response}")
                        
            if response.status_code == 200:
                try:
                    response_json = response.json()
                    logging.info(f"Response JSON: {response_json}")
                    content = response_json["choices"][0]["message"]["content"]
                    logging.info(f"Content retrieved: {content}")
                    clean = content.strip().strip("```").replace("json", "", 1).strip()
                    return json.loads(clean)

                except Exception as e:
                    logging.error(f"Error parsing response: {str(e)}")
                    logging.error(f"Response text: {response.text}")
                    return response.json()

            elif response.status_code == 401:
                logging.error("Error: Unauthorized - Invalid API key.")
                return None
            elif response.status_code == 404:
                logging.error("Error: The requested resource was not found.")
                return None
            else:
                logging.error(
                    f"Unexpected error occurred. Status code: {response.status_code}"
                )
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
                logging.error(
                    f"Unexpected error occurred. Status code: {response.status_code}"
                )
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
        data = {
            "chat": {
                "title": filename,
                "model": "gpt-oss:20b",
                "messages": [
                {
                    "role": "user",
                    "content": self.question,
                    "files": [
                    "file-" + file_id
                    ]
                }
                ],
                "folder_id": self.folder_id
            }
        }

        
        logging.info(f"Creating new chat with data: {data} at {full_url}")

        try:
            response = requests.post(full_url, headers=headers, json=data, timeout=50)
            if response.status_code == 200:
                logging.info(f"New chat response: {response.json()}")
                self.chat_id = response.json().get("id")
                return response.json()
            elif response.status_code == 401:
                logging.error("Error: Unauthorized - Invalid API key.")
                return None
            elif response.status_code == 404:
                logging.error("Error: The requested resource was not found.")
                return None
            else:
                logging.error(
                    f"Unexpected error occurred. Status code: {response.status_code}"
                )
                logging.error(f"Response text: {response.text}")
                return None
        except requests.exceptions.Timeout:
            logging.error("Request timed out after 50 seconds.")
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

        data = {"role": "assistant", "content": content}

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

    def getModels(self):
        endpoint = "/ollama/v1/models"
        full_url = urljoin(self.base_url, endpoint)
        headers = {"Authorization": f"Bearer {self.api_key}"}

        try:
            response = requests.get(full_url, headers=headers, timeout=10)
            response.raise_for_status()
            return response.json()

        except requests.exceptions.Timeout:
            logging.error("Timeout while fetching models.")
            return None

        except requests.exceptions.RequestException as e:
            logging.error(f"Error fetching models: {str(e)}")
            return None
