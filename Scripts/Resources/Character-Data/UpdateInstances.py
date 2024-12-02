import json
import argparse

def main():
    # Get the Input Arguements
    args = GetInputArgs()

    # Get the Template and instance file path's from the inputs args
    templatePath = args.template
    filePath = args.inputFile

    # Read in the JSON files as string
    rawTemplateData = GetFileAsString(templatePath)
    rawFileData = GetFileAsString(filePath)

    # Convert the JSON string to a JSON object
    templateData = json.loads(rawTemplateData)
    instanceData = json.loads(rawFileData)

    # Update the instance object to the template
    UpdateInstanceToTemplate(templateData, instanceData)

    # Save the updated instance
    with open(filePath, "w") as outfile: 
        json.dump(instanceData, outfile, indent=4)

def UpdateInstanceToTemplate(template, instance):
    
    for key, value in template.items():
        updatedValue = None
        if (type(value) is dict):
            updatedValue = {}
            UpdateInstanceToTemplate(value, updatedValue)
        else:
            updatedValue = value

        if (key not in instance.keys()):
            instance[key] = updatedValue

def GetFileAsString(filePath):
    with open(filePath, 'r') as file:
        fileContent = file.read()
    return fileContent

def GetInputArgs():
    parser = argparse.ArgumentParser()
    parser.add_argument('-inputFile', help='The character file that you want to update')
    parser.add_argument('-template', help='The reference template')
    return parser.parse_args()

if __name__ == "__main__":
    main()