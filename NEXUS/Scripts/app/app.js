var app = angular.module('nexus', [
    'ngCookies',
    'ui.router',                    // Routing
    'oc.lazyLoad',                  // ocLazyLoad
    'ui.bootstrap',                 // Ui Bootstrap
    'ui.bootstrap.datetimepicker',
    'ngIdle',  						 // Idle timer
    'imageupload',                    
    //'ui.select',
    'ngSanitize',                   // ngSanitize
    //'ngCsv',
    'ngAnimate',
    // 'ngCkeditor',
//    'rzTable',
    'ui.utils',
    'selectize',
    'ui.select'
]);

var API = "http://localhost:55555/api/nexus/";