'use strict';

angular.module('myApp.initView', ['ngRoute', 'xml'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/initView', {
    templateUrl: 'initView/initView.html',
    controller: 'initViewCtrl'
  });
}])

.controller('initViewCtrl', ['$scope', 'sharedProperties',function($scope, sharedProperties) {
    $scope.menuVisible = false;
    $scope.inputClick = function() {
      $scope.menuVisible = true; 
      sharedProperties.setUrl($scope.input);
    }
}]);