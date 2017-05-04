'use strict';

angular.module('myApp.initView', ['ngRoute', 'xml'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/initView', {
    templateUrl: 'initView/initView.html',
    controller: 'initViewCtrl'
  });
}])

.controller('initViewCtrl', ['$scope',function($scope) {
    $scope.menuVisible = false;
}]);