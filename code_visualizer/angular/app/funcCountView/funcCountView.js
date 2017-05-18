'use strict';

angular.module('myApp.funcCountView', ['ngRoute', 'xml'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/funcCountView', {
    templateUrl: 'funcCountView/funcCountView.html',
    controller: 'funcCountViewCtrl'
  });
}])

.controller('funcCountViewCtrl', ['x2js', '$scope', '$http', 'sharedProperties','$location', function(x2js, $scope, $http, sharedProperties,$location) {
    $scope.graphShow = false;
    var responseData;
    if(sharedProperties.getUrl() != '') {
        var req = {
            method: 'GET',
            url: sharedProperties.getApiUrl() + '/api/analyse/funcCount?repoUrl='+sharedProperties.getUrl(),
            headers: {
                'Content-Type': 'application/xml',
                'Accept': 'application/xml'
            },
            data: ''
        };
        console.log(req);
        $http(req).then(function succ(response) {
            responseData = x2js.xml_str2json(response.data);
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
        $scope.graphShow = true;
        $scope.loadingHide = true;
        console.log(responseObject.data);
        $scope.labels = [-90, -80, -70, -60, -50, -40, -30, -20, -10, 0];
        $scope.data = responseObject.data.reverse();
        $scope.options = {
            scales: {
            yAxes: [
                {
                id: 'y-axis-1',
                type: 'linear',
                display: true,
                position: 'left'
                }
            ]
            }
        };
        $scope.datasets = {
            fill: false,
            borderColor: "#CF5C36"
        }
    };

    


}]);