'use strict';

angular.module('myApp.importsView', ['ngRoute', 'xml'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/importsView', {
    templateUrl: 'importsView/importsView.html',
    controller: 'importsViewCtrl'
  });
}])

.controller('importsViewCtrl', ['x2js', '$scope', '$http', 'sharedProperties','$location', function(x2js, $scope, $http, sharedProperties,$location) {
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

    var fillChart = function() {
        $scope.loadingHide = true;
        console.log(responseObject.data);
        $scope.labels = [];
        $scope.data = [];
        console.log(responseObject.data[0]);
        for (var i = 0; i < responseObject.data[0].length; i++) { 
            console.log(responseObject.data[0][i]);
            console.log(responseObject.data[0][i].Key);
            console.log(responseObject.data[0][i].Value);
            $scope.labels.push(responseObject.data[0][i].Key);
            $scope.data.push(responseObject.data[0][i].Value);
        }
        
    };

    


}]);