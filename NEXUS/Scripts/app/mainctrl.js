function MainCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
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
            myEl = angular.element(document.querySelector('.username'));
            myEl.html(user.FullName);
            $scope.showBtnLog = true;
            $scope.showInfoLog = false;
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
            alert("dang ki thanh cong" + " " + data.data.Token)
        }, function (error) {
            console.log(error);
            alert(error.statusText);
        });
    }

    $scope.login = function () {
        var dataSend = {
            "PhoneNumber": $scope.logphone,
            "Password": $scope.logpass
        };
        xhrService.post("Login", dataSend).then(function (data) {
            console.log(data);
            alert("dang nhap thanh cong, xin chao" + " " + data.data.FullName)
            localStorage.setItem('user', JSON.stringify(data.data));
            location.reload();
        }, function (error) {
            console.log(error);
            alert(error.statusText);
        });
    }

    $scope.Logout = function () {
        localStorage.clear();
        location.reload();
    }
}

app.controller('MainCtrl', MainCtrl);
