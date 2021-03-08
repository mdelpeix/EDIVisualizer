# EDIVisualizer
EDIVisualizer is a reader for different files fomats use in EDI (Electronic data interchange).
EDIVisualizer use Plugin architecture to manage different formats. Actually, plugin are available for EDIFACT and VDA files. New plugins (i hope) will appear in futur (X12, specific formats).

Features :
  - Detect automatically the input file format and load the appropriate plugin
  - Can be launch automatically with file path argument
  - Search function in file in the current plugin selected
  - Drag and drop files function
  - Reset loading files
  - New Plugin creation possible (see [READMEPlugin.md](https://github.com/mdelpeix/EDIVisualizer/blob/master/READMEPlugin.md) for documentation and example)
  
Plugin Edifact :
  - Use by default to load file with "edi" extension
  - Load Information from http://www.stylusstudio.com

Plugin IDOC :
  - Use by default to load file with "idoc" extension
  - Highlight fields selected and diplay description
  - DELFOR02, DESADV01, INVOIC02, SEQJIT03 supported. New format can be supported by use transaction WE60 and put the result file in "Messages" folder in the installation directory.

Plugin VDA :
  - Use by default to load file with "vda" extension
  - Highlight fields selected and diplay description
  - 4905, 4915, 4908, 4913 supported. To make a new file support, make a xml file conform to PluginVDA.xsd supply in the PluginVda.csproj

Plugin Xml :
  - Use by default to load file with "xml" extension
  - Load xml file with the default view from Internet Explorer (So, the basic features are available with right-click)

Plugin Odette :
  - Use by default to load file with "odtt" extension
  - DELINSV3, INVOICV3, AVIEXPV3 supported. To make a new file support, make a xml file conform to PluginOdette.xsd supply in the PluginOdette.csproj

![Images](/Screenshots/edivisualizerScreenshotEdifact.jpg)
![Images](/Screenshots/edivisualizerScreenshotIDoc.jpg)
![Images](/Screenshots/edivisualizerScreenshotVDA.jpg)
![Images](/Screenshots/edivisualizerScreenshotXml.jpg)
