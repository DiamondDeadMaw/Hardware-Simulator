using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class Load_States : MonoBehaviour
{
    string dir_path;
    public IDictionary<string,IDictionary<string, List<string>>> savedStates;
    public List<string> fileNames;
    public List<string> startingInputNames;
    public List<string> startingOutputNames;
    IDictionary<string, List<string>> io;

    public void Start()
    {
        savedStates = new Dictionary<string,IDictionary<string, List<string>>>();         //to be accessed as savedStates[name][inputstates/outputstates/InputNames/OutputNames]
        fileNames = new List<string>();

        dir_path = Application.persistentDataPath;
        string [] file_paths = Directory.GetFiles(dir_path, "*.dat", SearchOption.TopDirectoryOnly);
        FileStream file;
        string name;
        foreach (string file_path in file_paths) {
            io = new Dictionary<string,List<string>>();


            file = File.OpenRead(file_path);
            BinaryFormatter bf = new BinaryFormatter();
            toSerialize data = (toSerialize) bf.Deserialize(file);
            name = System.IO.Path.GetFileName(file.Name);
            name = name.Substring(0, name.Length-4);
            fileNames.Add(name);      //fix this
            io.Add("InputStates", data.inputStates);
            io.Add("OutputStates", data.outputStates);
            startingInputNames = data.inputNames;
            startingOutputNames = data.outputNames;
            io.Add("InputNames", startingInputNames);
            io.Add("OutputNames", startingOutputNames);
            savedStates.Add(name, io);
            file.Close();
        }
    }

    public string getOutputState(string name, string inputState) {   //takes top to bottom input state and returns the corresponding output
        List<string> istates = savedStates[name]["InputStates"];
        for (int i = 0; i<istates.Count; i++) {
            if (inputState.Equals(istates[i])) {
                return (savedStates[name]["OutputStates"][i]);
            }
        }
        return "Error";
    }

    public void deleteFile(string fileName) {
        fileNames.Remove(fileName);
        File.Delete(Application.persistentDataPath + "/" + fileName +".dat");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
