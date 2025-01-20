from flask import Flask, jsonify, send_from_directory

app = Flask(__name__)

# Route for downloading settings.json
@app.route('/settings.json', methods=['GET'])
def get_settings():
    return send_from_directory('static', 'settings.json')

# For welcome.json
@app.route('/welcome.json', methods=['GET'])
def get_welcome():
    return send_from_directory('static', 'welcome.json')

# For AssetBundle
@app.route('/assetbundle/<filename>', methods=['GET'])
def get_asset_bundle(filename):
    return send_from_directory('static', filename)

if __name__ == '__main__':
    app.run(debug=True)