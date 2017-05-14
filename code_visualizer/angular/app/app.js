'use strict';

// Declare app level module which depends on views, and components
angular.module('myApp', [
  'chart.js',
  'ngRoute',
  'myApp.initView',
  'myApp.todoView',
  'myApp.rowCountView',
  'myApp.funcCountView',
  'myApp.importsView',
  'myApp.view2',
  'myApp.version'
])
.service('sharedProperties', function () {
        var url = '';
        var apiUrl = 'http://25.22.141.161:8080';

        return {
            getUrl: function () {
                return url;
            },
            setUrl: function(value) {
                url = value;
            },
            getApiUrl: function() {
              return apiUrl;
            }
        };
    })
.controller('mainCtrl', ['$scope','$route', '$routeParams', '$location',
  function mainCtrl($scope,$route, $routeParams, $location) {
    $scope.$route = $route;
    $scope.$location = $location;
    $scope.$routeParams = $routeParams;

    $scope.$on('$routeChangeStart', function(event, next, current){
    if($location.path() == '/initView') {
      $scope.menuVisible = false;
    } else {
      $scope.menuVisible = true;
    };
  });
}])
.config(['$locationProvider', '$routeProvider', function($locationProvider, $routeProvider) {
  $locationProvider.hashPrefix('!');

  $routeProvider.otherwise({redirectTo: '/initView'});
}]);
