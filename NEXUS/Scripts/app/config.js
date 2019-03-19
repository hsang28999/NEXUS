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
                abstract: true,
                templateUrl: "html/home.html"
            })
        .state('home.index',
            {
                url: "",
                templateUrl: "html/index.html"
            })
        .state('home.product',
            {
                url: "product",
                templateUrl: "html/product.html"
            })
        .state('home.productdetail',
            {
                url: "product-detail/:id",
                templateUrl: "html/productdetail.html"
            })
        .state('home.history',
            {
                url: "history",
                templateUrl: "html/history.html"
            })
        .state('home.userprofile',
            {
                url: "user-profile",
                templateUrl: "html/userprofile.html"
            })
        .state('home.pay',
            {
                url: "pay",
                templateUrl: "html/pay.html"
            })
        .state('home.aboutus',
            {
                url: "about-us",
                templateUrl: "html/aboutus.html"
        })
            .state('home.feedback',
                {
                    url: "feedback",
                    templateUrl: "html/feedback.html"
        })

        .state('login',
            {
                url: "/login",
                templateUrl: "html/login.html"
            })
        .state('admin',
            {
                url: "/admin",
                abstract: true,
                templateUrl: "html/admin.html"
        })
        .state('admin.employee',
                {
                    url: "/employee?page&search",
                    templateUrl: "html/employee.html"
        })
            .state('admin.employeedetail',
                {
                    url: "/employee-detail/{id}",
                    templateUrl: "html/employeedetail.html"
                })
            .state('admin.dashboard',
                {
                    url: "",
                    templateUrl: "html/dashboard.html"
                })
        .state('admin.contract',
            {
                url: "/contract?page&search",
                templateUrl: "html/contract.html"
            })
        .state('admin.contractdetail',
            {
                url: "/contract-detail/{id}",
                templateUrl: "html/contractdetail.html"
            })
        .state('admin.product',
            {
                url: "/product?page&search",
                templateUrl: "html/adminproduct.html"
            })
        .state('admin.productdetail',
            {
                url: "/product-detail/{id}",
                templateUrl: "html/adminproductdetail.html"
            })
        .state('admin.user',
            {
                url: "/user?page&search",
                templateUrl: "html/user.html"
            })
        .state('admin.userdetail',
            {
                url: "/user-detail/{id}",
                templateUrl: "html/userdetail.html"
            })
        .state('admin.shop',
            {
                url: "/shop?page&search",
                templateUrl: "html/shop.html"
            })
        .state('admin.shopdetail',
            {
                url: "/shop-detail/{id}",
                templateUrl: "html/shopdetail.html"
        })
            .state('admin.feedback',
                {
                    url: "/feedback",
                    templateUrl: "html/feedbackadmin.html"
        })
            .state('admin.feedbackdetail',
                {
                    url: "/feedback-detail/{id}",
                    templateUrl: "html/feedbackdetailadmin.html"
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