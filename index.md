# Documentation
This is documentation of public accesible api

## API endpoints

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
