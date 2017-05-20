# Funcionality
Features: 
- multiplatform
- clone any github repository
- display statistics and hints about codebase
- simple frontend with graphs for displaying statistics
- provide simple REST API
  - analyse/codeDebt  
   count TODOs in application, travelling 10 times 10 commits back on master
  - analyse/rowCount  
   count rows in apllication
  - analyse/funcCount  
   count number of functions
  - analyse/imports  
   count imports
  - analyse/params  
   count number of arguments and thier type
  - analyse/returnTypes
   count number and type of returns

# Team members Reviews

- [Peter Hrvola](https://retep007.github.io/code-visualizer/ph.html)

# Used packages excluding standard library
All can be easlly installed using nuget in ide. Either visual studio or mono develop.
- Web.Http.Cors
- srcML
- gitlib2sharp

# Running
App requires web server either IIS which is kind of trublesome or XSP (unix) or apache with mod_mono installed.

# Download built files
It's always better idea to compile project on your own as the ide will resolve missing dependencies (old .NET framework) anyway [link](https://retep007.github.io/code-visualizer/bin.zip)

# Documentation
This is documentation of public accesible api. Every api request requires repoUrl, if repository is large it might take a while to get response from a server. XML format is valid under microsoft xml standard. Api also support application/json type.

## API endpoints (WIP)

- *GET* api/analyse/todos?repoUrl={url}  
  result:  
  ```xml
  <versions>
    <version>
      <todo>5</todo>
    </version>
    <version>
      <todo>8</todo>
    </version>
    <version>
      <todo>10</todo>
    </version>
  </versions>
  ```
- *GET* api/analyse/arguments?repoUrl={url}  
  result:  
  ```xml
  <versions>
    <version>
      <argument type="int">5</argument>
      <argument type="bool">8</argument>
    </version>
    <version>
      <argument type="int">5</argument>
      <argument type="bool">8</argument>
    </version>
  </versions>
  ```
  
  
  
## Collaborators
Pavel Jezek, Peter Hrvola, Dusan Hetlerovic, Maria Michalikova
