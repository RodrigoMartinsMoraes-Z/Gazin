var app = angular.module('app', ['ngRoute']);
(function () {
    app.controller('developerController', ['$scope', '$http', '$route', '$routeParams', function (scope, http, route, routeParams) {
        apiUrl = "/api/developers";

        console.log(routeParams);

        scope.skip = 0;
        scope.take = 10;

        scope.getDevs = function () {
            http.get(`${apiUrl}skip=${scope.skip}&take=${scope.take}`)
                .then(function (r) {
                    scope.devs = r.data;
                })
        }

    }]);

    app.config(function ($routeProvider) {
        $routeProvider
            .when("/dev/:id", {
                templateUrl: "Templates/Home/dev-detail.html",
                params: { id: '' },
                controller: 'developerController'
            })
            .otherwise({
                templateUrl: "Templates/Home/index.html",
                controller: 'developerController'
            });

    });
})();