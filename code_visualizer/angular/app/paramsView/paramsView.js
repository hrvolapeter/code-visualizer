'use strict';

angular.module('myApp.paramsView', ['ngRoute', 'xml'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/paramsView', {
    templateUrl: 'paramsView/paramsView.html',
    controller: 'paramsViewCtrl'
  });
}])

.controller('paramsViewCtrl', ['x2js', '$scope', '$http', 'sharedProperties','$location','$compile', function(x2js, $scope, $http, sharedProperties,$location, $compile) {
    $scope.graphShow = false;
    var responseData;
    if(sharedProperties.getUrl() != '') {
        var req = {
            method: 'GET',
            url: sharedProperties.getApiUrl() + '/api/analyse/params?repoUrl=' + sharedProperties.getUrl(),
            headers: {
                'Content-Type': 'application/xml',
                'Accept': 'application/xml'
            },
            data: ''
        };
        console.log(req);
        $http(req).then(function succ(response) {
            responseData = x2js.xml_str2json(response.data);
            fillChart();
        }, function err(response) {
            console.log(response);
        });
    } else {
        $location.path("/");
    }

    var ls = [];
    var ds = [];

    var fillChart = function() {
        $scope.graphShow = true;
        $scope.loadingHide = true;

console.log(responseData);
        for (var i = 0; i < responseData.versions.version.length; i++) { 
            var lss = [];
            var dss = [];
            for(var j = 0; j < responseData.versions.version[i].arguments.length; j++) {
                lss.push(responseData.versions.version[i].arguments[j]._type);
                dss.push(responseData.versions.version[i].arguments[j].__text);
            }
            ls.push(lss);
            ds.push(dss);


        }
        
        $scope.labels = ls[0];
        $scope.data = ds[0];

        $scope.datasets = {
            fill: false,
            borderColor: "#CF5C36"
        }
        
    };

    $scope.updateChart = function(value) {
        $scope.labels = ls[value];
        $scope.data = ds[value];
    }

    


}]);