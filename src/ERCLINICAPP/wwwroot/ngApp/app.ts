namespace ERCLINICAPP {

    angular.module('ERCLINICAPP', ['ui.router', 'ngResource', 'ui.bootstrap']).config((
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider,
        $locationProvider: ng.ILocationProvider
    ) => {
        // Define routes
        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: '/ngApp/views/home.html',
                controller: ERCLINICAPP.Controllers.HomeController,
                controllerAs: 'controller'
            })
            .state('secret', {
                url: '/secret',
                templateUrl: '/ngApp/views/secret.html',
                controller: ERCLINICAPP.Controllers.SecretController,
                controllerAs: 'controller'
            })
            .state('login', {
                url: '/login',
                templateUrl: '/ngApp/views/login.html',
                controller: ERCLINICAPP.Controllers.LoginController,
                controllerAs: 'controller'
            })
            .state('register', {
                url: '/register',
                templateUrl: '/ngApp/views/register.html',
                controller: ERCLINICAPP.Controllers.RegisterController,
                controllerAs: 'controller'
            })
            .state('externalRegister', {
                url: '/externalRegister',
                templateUrl: '/ngApp/views/externalRegister.html',
                controller: ERCLINICAPP.Controllers.ExternalRegisterController,
                controllerAs: 'controller'
            }) 
            .state('addClinic', {
                url: '/addClinic',
                templateUrl: '/ngApp/views/addClinic.html',
                controller: ERCLINICAPP.Controllers.AddClinicController,
                controllerAs: 'controller'
            })
            .state('editClinic', {
                url: '/editClinic/:id',
                templateUrl: '/ngApp/views/editClinic.html',
                controller: ERCLINICAPP.Controllers.EditClinicController,
                controllerAs: 'controller'
            })
            .state('deleteClinic', {
                url: '/deleteClinic/:id',
                templateUrl: '/ngApp/views/deleteClinic.html',
                controller: ERCLINICAPP.Controllers.DeleteClinicController,
                controllerAs: 'controller'
            })




            .state('notFound', {
                url: '/notFound',
                templateUrl: '/ngApp/views/notFound.html'
            });

        // Handle request for non-existent route
        $urlRouterProvider.otherwise('/notFound');

        // Enable HTML5 navigation
        $locationProvider.html5Mode(true);
    });

    
    angular.module('ERCLINICAPP').factory('authInterceptor', (
        $q: ng.IQService,
        $window: ng.IWindowService,
        $location: ng.ILocationService
    ) =>
        ({
            request: function (config) {
                config.headers = config.headers || {};
                config.headers['X-Requested-With'] = 'XMLHttpRequest';
                return config;
            },
            responseError: function (rejection) {
                if (rejection.status === 401 || rejection.status === 403) {
                    $location.path('/login');
                }
                return $q.reject(rejection);
            }
        })
    );

    angular.module('ERCLINICAPP').config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    });

    

}
