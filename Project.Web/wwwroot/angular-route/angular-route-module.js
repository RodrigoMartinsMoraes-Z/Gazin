var app = angular.module("app", ["ngRoute"]);
app.config(function ($routeProvider) {
    $routeProvider       
        .when("/login", {
            templateUrl: "Templates/Login/index.html"
        })
        .otherwise({
            templateUrl: "Templates/Home/index.html"
        });

});