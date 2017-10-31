(function () {
    //Cria um Module 
    // será usado ['ng-Route'] quando implementarmos o roteamento
    var app = angular.module('TableOfWeb', ['ngRoute'])
        .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/Registration/Courses', { templateUrl: '/templates/Courses/courses.html', controller: 'CoursesController' });
        $routeProvider.when('/Registration/Students', { templateUrl: '/templates/Students/students.html', controller: 'StudentsController' });
        $locationProvider.html5Mode(true);
    });
    //Cria um Controller
    // aqui $scope é usado para compartilhar dados entre a view e o controller
    app.controller('ContaController', function ($scope, $http) {
        $scope.Mensagem = "Teste de Conta";

        $scope.keyword = '';

        $scope.GetAllData = function () {
            $http.get('/Conta/GetData')
            .success(function (data, status, headers, config) {
                $scope.Details = data;
                $scope.Mensagem = data;
            })
            .error(function (data, status, header, config) {
                $scope.ResponseDetails = "Data: " + data +
                    "<br />status: " + status +
                    "<br />headers: " + jsonFilter(header) +
                    "<br />config: " + jsonFilter(config);
            });
        };

        $scope.SearchData = function () {

            var parameters = {
                keyword: $scope.keyword
            };
            var config = {
                params: parameters
            };

            $http.get('/ServerRequest/GetData', config)
            .success(function (data, status, headers, config) {
                $scope.Details = data;
            })
            .error(function (data, status, header, config) {
                $scope.ResponseDetails = "Data: " + data +
                    "<hr />status: " + status +
                    "<hr />headers: " + header +
                    "<hr />config: " + config;
            });
        };
    });
})();