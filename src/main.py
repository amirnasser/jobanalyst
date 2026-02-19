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
    format="%(asctime)s [%(levelname)s] %(message)s"
)

os.makedirs(settings.get("upload_folder", "uploads"), exist_ok=True)


folder_id = ""
chat_id = ""

def allowed_file(self, filename):
    return "." in filename and filename.rsplit(".", 1)[1].lower() in settings.get("allowd_extensions", [])
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

            if file: # and allowed_file(file.filename):
                filename = secure_filename(datetime.datetime.now().strftime("%Y%m%d-") + file.filename)
                filepath = os.path.join(settings.get("upload_folder", "uploads"), filename)
                logging.info(f"Filepath: {filepath} ")
                file.save(filepath)
                api_response = OpenWebUI_instance.upload_file(filename)
                logging.info(f"API response: {api_response}")
                return jsonify(api_response), 200
        except Exception as e:
            logging.error(f"Error during file upload: {str(e)}")
            return jsonify({"error": "An error occurred during file upload"}), 500

@app.errorhandler(404)
def page_not_found(error):
    return jsonify({"error": "Resource not found"}), 404

@app.errorhandler(500)
def internal_error(error):
    return jsonify({"error": "Internal server error"}), 500




if __name__ == "__main__":
    app.run(port=5000)

