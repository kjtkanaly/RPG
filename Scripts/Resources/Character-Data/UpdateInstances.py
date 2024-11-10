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
    with open("Resources/Character-Data/Allies/dummy-test.json", "w") as outfile: 
        json.dump(instanceData, outfile, indent=4)


def UpdateInstanceToTemplate(template, instance):
    
    for key, value in template.items():
        if (type(value) is dict):
            subInstance = {}
            UpdateInstanceToTemplate(value, subInstance)
            instance[key] = subInstance
        else:
            print(key, value)
            instance[key] = value


def ClimbThroughDict(dictionary):
    for key, value in dictionary.items():
        print(key)
        if (type(value) is dict):
            ClimbThroughDict(value)

def GetFileAsString(filePath):
    with open(filePath, 'r') as file:
        fileContent = file.read()
    return fileContent

if __name__ == "__main__":
    main()