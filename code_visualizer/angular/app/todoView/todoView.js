'use strict';

angular.module('myApp.todoView', ['ngRoute', 'xml'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/todoView', {
    templateUrl: 'todoView/todoView.html',
    controller: 'todoViewCtrl'
  });
}])

.controller('todoViewCtrl', ['x2js', '$scope', '$http', function(x2js, $scope, $http) {
    $scope.input = "";

    $scope.initRepositories = function() {
    /* TODO: validate urls */
    var url = $scope.input;
    var jsonObj = {
        'Repositories' : {
            '_xmlns': 'http://schemas.datacontract.org/2004/07/',
            '_xmlns:s': 'http://schemas.microsoft.com/2003/10/Serialization/Arrays',
            '_xmlns:i': 'http://www.w3.org/2001/XMLSchema-instance',
            'Repository' : {
              's:string': url
            }
        }
    };
    console.log(jsonObj);
    var xmlAsStr = '<?xml version="1.0" encoding="utf-8" ?>'+x2js.json2xml_str( jsonObj );

    console.log(xmlAsStr);

    var req = {
        method: 'POST',
        url: 'http://localhost:56019/api/analyse/codeDebt?repoUrl',
        headers: {
            'Content-Type': 'application/xml'
        },
        data: xmlAsStr
    };
    console.log("new");
    console.log(req);
    $http(req).success(function (response) {
        alert(response);
    });
    console.log("function ended");
  }
}]);