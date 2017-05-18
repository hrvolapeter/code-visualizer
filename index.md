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

# Documentation
This is documentation of public accesible api. Every api request requires repoUrl, if repository is large it might take a while to get response from a server. XML format is valid under microsoft xml standard. Api also support application/json type.

## Validation
API validation schema is available in project source codes.

## API endpoints

- count Todos in repo  
  *GET* api/analyse/codeDebt?repoUrl={url}&diff={5?}&times={3?}
  result:  
  ```xml
  <versions>
  <version>
    <todo>0</todo>
  </version>
  <version>
    <todo>0</todo>
  </version>
  <version>
    <todo>0</todo>
  </version>
  </versions>
  ```
- count rows in repo  
  *GET* api/analyse/rowCount?repoUrl={url}&diff={3?}&times={10?}
  result:  
  ```xml
  <versions>
  <version>
    <rows>544</rows>
  </version>
  <version>
    <rows>275</rows>
  </version>
  <version>
    <rows>48</rows>
  </version>
  </versions>
  ```
- count functions in repo  
  *GET* api/analyse/funcCount?repoUrl={url}&diff={3?}&times={10?}
  result:  
  ```xml
  <versions>
  <version>
    <function>22</function>
  </version>
  <version>
    <function>11</function>
  </version>
  <version>
    <function>2</function>
  </version>
  </versions>
  ```
- count imports in repo and display types  
  *GET* api/analyse/imports?repoUrl={url}&diff={3?}&times={10?}
  result:  
  ```xml
  <versions>
    <version>
      <import name="System">4</import>
      <import name="System.Collections.Generic">3</import>
      <import name="System.Web.Http">3</import>
      <import name="System.Linq">2</import>
      <import name="System.IO">2</import>
      <import name="System.Web.Http.Cors">1</import>
      <import name="System.Xml">1</import>
      <import name="System.Diagnostics">1</import>
      <import name="System.Web.Routing">1</import>
      <import name="System.Web">1</import>
      <import name="System.Xml.XPath">1</import>
    </version>
    <version>
      <import name="System">3</import>
      <import name="System.Web.Http">3</import>
      <import name="System.IO">2</import>
      <import name="System.Collections.Generic">2</import>
      <import name="System.Linq">1</import>
      <import name="System.Web.Http.Cors">1</import>
      <import name="System.Xml">1</import>
      <import name="System.Web.Routing">1</import>
      <import name="System.Web">1</import>
      <import name="System.Diagnostics">1</import>
    </version>
    <version>
      <import name="System.Web.Http">2</import>
      <import name="System.Web">1</import>
    </version>
  </versions>
  ```
- count params and display their types  
  *GET* api/analyse/params?repoUrl={url}&diff={3?}&times={10?}
  result:  
  ```xml
  <versions>
    <version>
      <arguments type="string">16</arguments>
      <arguments type="uint">6</arguments>
      <arguments type="int">5</arguments>
      <arguments type="ArgumentException">3</arguments>
      <arguments type="Object">1</arguments>
      <arguments type="XPathNavigator">1</arguments>
      <arguments type="HttpConfiguration">1</arguments>
      <arguments type="EventArgs">1</arguments>
      <arguments type="XmlNode">1</arguments>
    </version>
    <version>
      <arguments type="string">7</arguments>
      <arguments type="Object">1</arguments>
      <arguments type="EventArgs">1</arguments>
      <arguments type="HttpConfiguration">1</arguments>
      <arguments type="uint">1</arguments>
    </version>
    <version>
      <arguments type="HttpConfiguration">1</arguments>
    </version>
  </versions>
  ```
- count loopsCount and dispplay by type  
  *GET* api/analyse/loopsCount?repoUrl={url}&diff={3?}&times={10?}
  result:  
  ```xml
  <versions>
    <version>
      <loop type="for">9</loop>
      <loop type="foreach">6</loop>
      <loop type="while">1</loop>
      <loop type="do">0</loop>
    </version>
    <version>
      <loop type="foreach">3</loop>
      <loop type="for">2</loop>
      <loop type="while">0</loop>
      <loop type="do">0</loop>
    </version>
    <version>
      <loop type="for">0</loop>
      <loop type="foreach">0</loop>
      <loop type="while">0</loop>
      <loop type="do">0</loop>
    </version>
  </versions>
  ```
- count if in repo
  *GET* api/analyse/ifCount?repoUrl={url}&diff={3?}&times={10?}
  result:  
  ```xml
  <versions>
    <version>
      <if>10</if>
    </version>
    <version>
      <if>6</if>
    </version>
    <version>
      <if>0</if>
    </version>
  </versions>
  ```

## Collaborators
Pavel Jezek, Peter Hrvola, Dusan Hetlerovic, Maria Michalikova
