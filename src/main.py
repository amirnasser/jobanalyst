from flask import Flask, request, jsonify
import os
from urllib.parse import urljoin
from open_webui_client import OpenWebUI
import logging
import datetime

app = Flask(__name__)
OpenWebUI_instance = OpenWebUI()
settings = OpenWebUI_instance.get_settings()

logging.basicConfig(
    filename="app.log",
    level=logging.INFO,
    format="%(asctime)s [%(levelname)s] %(message)s",
)

os.makedirs(settings.get("upload_folder", "uploads"), exist_ok=True)


folder_id = ""
chat_id = ""


def allowed_file(self, filename):
    return "." in filename and filename.rsplit(".", 1)[1].lower() in settings.get(
        "allowd_extensions", []
    )


def secure_filename(filename):
    return filename.replace(" ", "_").replace("/", "_")


@app.route("/folders", methods=["GET"])
def getfolders():
    folders = OpenWebUI_instance.get_folders()
    if folders is not None:
        return jsonify(folders), 200
    else:
        return jsonify({"error": "Failed to fetch folders"}), 500


@app.route("/", methods=["GET"])
def version():
    return jsonify({"version": "1.0.0"}), 200


@app.route("/upload", methods=["POST"])
def upload():
    if request.method == "POST":
        try:
            # Check if the post request has a file part
            if "file" not in request.files:
                return jsonify({"error": "No file selected"}), 400

            file = request.files["file"]

            if file.filename == "":
                return jsonify({"error": "No file selected"}), 400


            if file:  # and allowed_file(file.filename):
                filename = secure_filename(
                    datetime.datetime.now().strftime("%Y%m%d-") + file.filename
                )
                filepath = os.path.join(
                    settings.get("upload_folder", "uploads"), filename
                )
                logging.info(f"Filepath: {filepath} ")
                file.save(filepath)

                # upload file to OpenWebUI
                api_response = OpenWebUI_instance.upload_file(filename)
                logging.info(f"API response: {api_response}")

                if api_response is not None and "id" in api_response:
                    file_id = api_response.get("id")
                    logging.info(f"File uploaded successfully. File ID: {file_id}")

                    # Create a new chat with the uploaded file
                    # new_chat_response = OpenWebUI_instance.create_new_chat(api_response.get("id"), filename)
                    new_chat_response = OpenWebUI_instance.create_new_chat(
                        file_id, filename
                    )
                    logging.info(f"New chat response: {new_chat_response}")

                    if new_chat_response is not None:
                        logging.info(
                            f"Creating Completions for chat ID: {new_chat_response.get('id')}"
                        )
                        get_completions_response = OpenWebUI_instance.get_completions(
                            file_id
                        )

                        if get_completions_response is not None:
                            logging.info(
                                f"Completions done successfully: {get_completions_response})"
                            )
                            logging.info(
                                f"Deleting chat with ID: {new_chat_response.get('id')}"
                            )
                            delete_chat_response = OpenWebUI_instance.delete_chat()
                            if delete_chat_response is not None:
                                logging.info(
                                    f"Chat deleted successfully: {delete_chat_response}"
                                )
                            return jsonify(get_completions_response), 200

        except Exception as e:
            logging.error(f"Error during file upload: {str(e)}")
            return jsonify({"error": "An error occurred during file upload"}), 500


@app.route("/models", methods=["GET"])
def get_models():
    models = OpenWebUI_instance.getModels()
    if models is not None:
        return jsonify(models), 200
    else:
        return jsonify({"error": "Failed to fetch models"}), 500


@app.errorhandler(404)
def page_not_found(error):
    return jsonify({"error": "Resource not found"}), 404


@app.errorhandler(500)
def internal_error(error):
    return jsonify({"error": "Internal server error"}), 500


if __name__ == "__main__":
    app.run(port=5000, debug=True)
