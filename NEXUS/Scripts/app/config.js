
function config($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, $locationProvider, IdleProvider) {

    // Configure Idle settings
    IdleProvider.idle(5); // in seconds
    IdleProvider.timeout(120); // in seconds

    $urlRouterProvider.otherwise("/");

    $ocLazyLoadProvider.config({
        // Set to true if you want to see what and when is dynamically loaded
        debug: false
    });
    $locationProvider.html5Mode(true);

    $stateProvider

        
        .state('home',
            {
                url: "/",
                templateUrl: "html/index.html"
            })
        .state('product',
            {
                url: "/product",
                templateUrl: "html/product.html"
        })
            .state('productdetail',
                {
                    url: "/product-detail/:id",
                    templateUrl: "html/productdetail.html"
                })
        .state('history',
            {
                url: "/history",
                templateUrl: "html/history.html"
            })
        .state('userprofile',
            {
                url: "/user-profile",
                templateUrl: "html/userprofile.html"
        })
            .state('pay',
                {
                    url: "/pay",
                    templateUrl: "html/pay.html"
        })
            .state('aboutus',
                {
                    url: "/about-us",
                    templateUrl: "html/aboutus.html"
                })
        ;
}


app.config(config)
    .run(function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
        $rootScope.$on('$stateChangeSuccess', function () {
            document.body.scrollTop = document.documentElement.scrollTop = 0;

        });
    });
