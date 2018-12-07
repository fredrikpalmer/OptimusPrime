


# OptimusPrime #

Transform config files to selected environment

## Run app ##

In windows shell: Navigate to installed location

transform, Command  
-p, Path to project root  
-e, Transform config files to environment  
--exclude, Exclude files from transformation  
--override, Override certain elements in the config files  

Example: 
optimusprime transform -p C:\dev\repos\MyProject\ -e Production --exclude custom.config --override

## Add to external tools in visual studio ##

Open Visual Studio

Goto Tools >> External Tools... in menubar

In the dialog click Add  
Title: optimusprime  
Command: [Path to tool]\OptimusPrime\OptimusPrime.XmlTransform\bin\Debug\optimusprime.exe    
Arguments: transform -p $(ProjectDir) -e Production --exclude custom.config --override  
Initial directory: [Path to tool]\OptimusPrime\OptimusPrime.XmlTransform\bin\Debug\  

Check Use Output window and click apply and close dialog

Tool will be available in Tools menu option  