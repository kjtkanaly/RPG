import json

def main():
    templatePath = "Resources/Character-Data/template.json"
    filePath = "Resources/Character-Data/Allies/dummy.json"

    rawTemplateData = GetFileAsString(templatePath)
    rawFileData = GetFileAsString(filePath)

    templateData = json.loads(rawTemplateData)
    instanceData = json.loads(rawFileData)

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

if __name__ == "__main__":
    main()