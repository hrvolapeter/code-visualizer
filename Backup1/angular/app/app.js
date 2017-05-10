'use strict';

// Declare app level module which depends on views, and components
angular.module('myApp', [
  'ngRoute',
  'myApp.initView',
  'myApp.todoView',
  'myApp.view2',
  'myApp.version'
])
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
