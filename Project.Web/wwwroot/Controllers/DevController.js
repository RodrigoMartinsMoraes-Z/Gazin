var app = angular.module('app', ['ngRoute']);
(function () {
    app.controller('developerController', ['$scope', '$http', '$route', '$routeParams', '$window', function (scope, http, route, routeParams, window) {
        apiUrl = "/api/developers";

        scope.skip = 0;
        scope.take = 10;
        scope.name = '';
        scope.hobby = '';

        scope.getDevs = function () {
            http.get(`${apiUrl}?name=${scope.name}&hobby=${scope.hobby}&skip=${scope.skip}&take=${scope.take}`)
                .then(function (r) {
                    scope.devs = r.data;
                });
        };

        scope.newDevPage = function () {
            window.location.href = '#!/dev';
        };

        scope.details = function (id) {
            window.location.href = '#!/dev/' + id;
        }

        scope.getDev = function () {
            if (routeParams.id > 0) {
                http.get(`${apiUrl}/${routeParams.id}`)
                    .then(function (r) {
                        scope.dev = r.data;
                        scope.dev.birthDate = new Date(r.data.birthDate);

                        console.log(scope.dev);
                    })
            }
        };

        scope.saveDev = function () {
            if (routeParams.id > 0)
                updateDev();
            else
                newDev();
        };

        updateDev = function () {
            http.put(`${apiUrl}/${routeParams.id}`, scope.dev)
                .then(function () {
                    alert("Informações atualizadas com sucesso.");
                })
                .catch(function () {
                    alert("Ocorreu um problema com a requisição, se o problema persistir contate um desenvolvedor")
                });
        };

        newDev = function () {
            http.post(`${apiUrl}`, scope.dev)
                .then(function () {
                    alert("Informações atualizadas com sucesso.");
                })
                .catch(function () {
                    alert("Ocorreu um problema com a requisição, se o problema persistir contate um desenvolvedor")
                });
        };

        scope.deleteDev = function () {
            http.delete(`${apiUrl}/${routeParams.id}`)
                .then(function () {
                    alert("Dev excluido");
                    window.location.href = 'home';
                });
        }

    }]);

    app.directive('dateInput', function () {
        return {
            restrict: 'A',
            scope: {
                ngModel: '='
            },
            link: function (scope) {
                if (scope.ngModel) scope.ngModel = new Date(scope.ngModel);
            }
        }
    })

    app.config(function ($routeProvider) {
        $routeProvider
            .when("/dev/", {
                templateUrl: "Templates/Home/dev-detail.html",
                controller: 'developerController'
            })
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