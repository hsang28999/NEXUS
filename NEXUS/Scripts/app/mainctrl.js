function MainCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    toastr.options = {
        "positionClass": "toast-bottom-right",
    }
    var user = JSON.parse(localStorage.getItem('user'));
    $scope.loadLayout = function() {
        //if (!(localStorage && localStorage.getItem('admin'))) {
        //    window.location.href = "/login";
        //};

        if (user === null) {
            $scope.showBtnLog = false;
            $scope.showInfoLog = true;
        }
        else {
            $scope.dispRole = user.Role;
            $scope.dispName = user.FullName;
            $scope.showBtnLog = true;
            $scope.showInfoLog = false;
        }

        if (user.Role != 1) {
            window.location.href = "/admin";
        } 
    }

    $scope.test = function() {
            xhrService.get("GetAccountDetail/1").then(function (data) {
                console.log(data);
            }, function (error) {
                $scope.errorText = "Tài khoản hoặc mật khẩu sai";
            });
        }

    $scope.replaceString = function (str) {
        if (!str)
            return null;
        str = str.toLowerCase();
        str = str.replace(/\ /g, "-");
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
        str = str.replace(/đ/g, "d");
        str = str.replace(/\”|\“|\"|\[|\]|\?/g, "");
        return str;
    };

    $scope.register = function () {
        var dataSend = {
            "PhoneNumber": $scope.regphone,
            "Email": $scope.regemail,
            "Password": $scope.regpass,
            "FullName": $scope.regfullname
        };
        xhrService.post("Register", dataSend).then(function (data) {
            var myToast = toastr.success('Auto login after 5 second', 'Register Success', { timeOut: 0 });
            var n = 5;
            setTimeout(countDown, 1000);
            function countDown() {
                n--;
                if (n > 0) {
                    setTimeout(countDown, 1000);
                }
                myToast.find(".toast-message").text('Auto login after ' + n + ' second');
            }
            setTimeout(() => {   
                localStorage.clear();
                localStorage.setItem('user', JSON.stringify(data.data));
                location.reload();
            }, 5000);
        }, function (error) {
            console.log(error);
            toastr.error(error.statusText, 'Error');
        });
    }

    $scope.login = function () {
        var dataSend = {
            "PhoneNumber": $scope.logphone,
            "Password": $scope.logpass
        };
        xhrService.post("Login", dataSend).then(function (data) {
            console.log(data);
            localStorage.setItem('user', JSON.stringify(data.data));
            toastr.success('Welcome' + ' ' + data.data.FullName, 'Login Success');
            setTimeout(() => {
                if (data.data.Role != 1) {
                    window.location.href = "/admin";  
                }
                else {
                    window.location.href = "/";  
                }
            }, 2000);
        }, function (error) {
            
            toastr.error(error.statusText, 'Error');
            console.log(error);
        });
    }

    $scope.Logout = function () {
        if (confirm('Do you want to logout ?')) {
            localStorage.clear();
            location.reload();
            window.location.href = "/";
        }
        
    }
}

app.controller('MainCtrl', MainCtrl);
