function ContractdetailAPCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    var id = $stateParams.id;
    myEl = angular.element(document.querySelector('.header2'));
    toastr.options = {
        "positionClass": "toast-bottom-right",
    }

    $scope.loadContractdetailAP = function () {

        $scope.AllStatus = [
            { value: 1, name: "Activated" },
            { value: 2, name: "	Not Activated" }
        ];

        xhrService.get("GetListUser/1").then(function (data) {
            $scope.AllUser = data.data.data;
        }, function (error) {

        });

        xhrService.get("GetListProductAdmin/1").then(function (data) {
            $scope.AllProduct = data.data.data;
        }, function (error) {

        });

        xhrService.get("GetListEmployee/1").then(function (data) {
            $scope.AllEmployee = data.data.data;
        }, function (error) {

        });

        xhrService.get("GetListStore").then(function (data) {
            $scope.AllStore = data.data;
        }, function (error) {

            });
        
        function getTextDayBySecond(second) {
            var d = Number(second);
            var h = Math.floor(d / 3600);
            var m = Math.floor(d % 3600 / 60);
            var s = Math.floor(d % 3600 % 60);

            var hDisplay = h > 0 ? h + (h == 1 ? " hour " : " hours ") : "";
            var mDisplay = m > 0 ? m + (m == 1 ? " ,minute " : " ,minutes ") : "";
            var sDisplay = s > 0 ? s + (s == 1 ? " ,second" : " ,seconds") : "";

            return hDisplay + mDisplay + sDisplay;
        }


        $scope.AllType = [
            { value: 1, name: "Prepaid By Hour" },
            { value: 2, name: "Unlimited Prepaid" },
            { value: 3, name: "Postpaid" }
        ];

        xhrService.get("GetListConnectionGroup").then(function (data) {
            $scope.AllGroup = data.data;
        }, function (error) {

        });

        if (id == undefined || id == null || id == "") {
            $scope.btnUpdate = true;
            $scope.inpHide = true;
            $scope.btnAdd = false;
            myEl.html("New Contract");
        }
        else {
            $scope.btnAdd = true;
            $scope.disuser = true;
            $scope.btnUpdate = false;
            myEl.html("Edit Contract");          
            xhrService.get("GetContractDetail/" + id).then(function (data) {
                $scope.user = data.data.UserId;
                $scope.employee = data.data.EmployeeId;
                $scope.product = data.data.ProductId;
                $scope.store = data.data.StoreId;
                $scope.note = data.data.Note;
                $scope.signing = new Date(data.data.SigningDate * 1000);
                $scope.status = data.data.Status;
                $scope.price = data.data.Price + " $";
                $scope.start = new Date(data.data.StartDate * 1000);
                $scope.end = new Date(data.data.EndDate * 1000);
                $scope.type = data.data.ProductType;
                $scope.time = getTextDayBySecond(data.data.ProductTimeUsed);
                $scope.local = data.data.PpmLocal;
                $scope.std = data.data.PpmStd;
                $scope.mobile = data.data.PpmMobile;
                $scope.address = data.data.Address;
                $scope.sec = data.data.SecurityDeposit;
              
                $scope.used = getTextDayBySecond(data.data.TimeUsedAvailable);
            }, function (error) {

            });
        }

        
    }

    $scope.Update = function () {
        var dataSend = {
            "Note": $scope.note,
            "Status": $scope.status
        }
        xhrService.get("GetContractDetail/" + id).then(function (data) {
            xhrService.post("SaveContract/" + id, dataSend).then(function (data) {
                toastr.success('Save done !', 'Success');
                setTimeout(() => {
                    window.location.href = "admin/contract";
                }, 2000);
            }, function (error) {
                toastr.error(error.statusText, 'Error');
            });
        }, function (error) {
            toastr.error(error.statusText, 'Error');
        });
    }

    $scope.Add = function () {
        var date = new Date($scope.start).getTime() / 1000;
        var dataSend = {
            "Status": 1,
            "StartDate": date,
            "Price": $scope.price,
            "Note": $scope.note,
            "StoreId": $scope.store,
            "EmployeeId": $scope.employee,
            "UserId": $scope.user,
            "ProductId": $scope.product
        };
        if ($scope.price == undefined || $scope.note == undefined
            || $scope.store == undefined || $scope.employee == undefined || $scope.user == undefined || $scope.product == undefined) {
            $scope.msgErr = true;
        }
        else {
            $scope.msgErr = false;
            xhrService.post("CreateContract", dataSend).then(function (data) {
                toastr.success('Add new contract done !', 'Success');
                setTimeout(() => {
                    window.location.href = "admin/contract";
                }, 2000);
            }, function (error) {
                toastr.error(error.statusText, 'Error');
            });
        }
    }

    $scope.Delete = function () {
        toastr.error("dAnG phAt tRiEn ?", 'Loi');
    }
}

app.controller('ContractdetailAPCtrl', ContractdetailAPCtrl);
