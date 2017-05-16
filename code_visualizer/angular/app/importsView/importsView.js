'use strict';

angular.module('myApp.importsView', ['ngRoute', 'xml'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/importsView', {
    templateUrl: 'importsView/importsView.html',
    controller: 'importsViewCtrl'
  });
}])

.controller('importsViewCtrl', ['x2js', '$scope', '$http', 'sharedProperties','$location', function(x2js, $scope, $http, sharedProperties,$location) {
    $scope.graphShow = false;
    var responseObject;
    if(sharedProperties.getUrl() != '') {
        var req = {
            method: 'GET',
            url: sharedProperties.getApiUrl() + '/api/analyse/imports?repoUrl=' + sharedProperties.getUrl(),
            headers: {
                'Content-Type': 'application/xml'
            }
        };
        console.log(req);
        $http(req).then(function succ(response) {
            responseObject = response;
            console.log(response);
            fillChart();
        }, function err(response) {
            console.log(response);
        });
        console.log("function ended");
    } else {
        $location.path("/");
    }

    var ls = [];
    var ds = [];

    var fillChart = function() {
        $scope.graphShow = true;
        $scope.loadingHide = true;
        var menu = document.getElementById("selectMenu");
        
        var index = -90;
        for (var i = 0; i < responseObject.data.length; i++) { 
            var lss = [];
            var dss = [];
            for(var j = 0; j < responseObject.data[i].length; j++) {
                lss.push(responseObject.data[i][j].Key);
                dss.push(responseObject.data[i][j].Value);
            }
            ls.push(lss);
            ds.push(dss);
            var btn = document.createElement("button");
            var node = document.createTextNode(index);
            btn.appendChild(node);
            index += 10;
            var attr = document.createAttribute("ng-click");
            //btn.appendChild(attr);
            btn.setAttribute("ng-click","updateChart(" + i +");");
            menu.appendChild(btn);
        }
        $scope.labels = ls[0];
        $scope.data = ds[0];

        $scope.datasets = {
            fill: false,
            borderColor: "#CF5C36"
        }
        
    };

    var updateChart = function(value) {
        $scope.labels = ls[value];
        $scope.data = ds[value];
    }

    


}]);