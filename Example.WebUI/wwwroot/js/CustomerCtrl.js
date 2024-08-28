
app.controller('CustomerCtrl', ['$scope', 'CrudService',
    function ($scope, CrudService) {
        // Base Url 
        var baseUrl = 'https://localhost:7059//api/customer/';
        $scope.btnText = "Save";
        $scope.SaveUpdate = function () {
            var customerModel = {
                Name: $scope.name,
                Address: $scope.address,
                Age: $scope.age,
                Email: $scope.email,
                Phone: $scope.phone,
                CustomerId: $scope.customerId
            }
            if ($scope.btnText == "Save") {
                var apiRoute = baseUrl;
                var saveCustomer = CrudService.post(apiRoute, customerModel);
                saveCustomer.then(function (response) {
                    $scope.GetCustomers();
                    $scope.Clear();
                }, function (error) {
                    console.log("Error: " + error);
                });
            }
            else {
                var apiRoute = baseUrl;
                var UpdateCustomer = CrudService.put(apiRoute, customerModel);
                UpdateCustomer.then(function (response) {
                        $scope.GetCustomers();
                        $scope.Clear();
                }, function (error) {
                    console.log("Error: " + error);
                });
            }
        }


        $scope.GetCustomers = function () {
            var apiRoute = baseUrl;
            var customerModel = CrudService.getAll(apiRoute);
            customerModel.then(function (response) {
                $scope.customers = response.data;
            },
                function (error) {
                    console.log("Error: " + error);
                });
        }
        $scope.GetCustomers();

        $scope.GetCustomerByID = function (dataModel) {
            var apiRoute = baseUrl;
            var customerModel = CrudService.getbyID(apiRoute, dataModel.customerId);

            customerModel.then(function (response) {
                $scope.name = response.data.name;
                $scope.address = response.data.address;
                $scope.age = response.data.age;
                $scope.email = response.data.email;
                $scope.phone = response.data.phone;
                $scope.customerId = response.data.customerId;
                $scope.btnText = "Update";
            },
                function (error) {
                    console.log("Error: " + error);
                });
        }

        $scope.DeleteCustomer = function (dataModel) {
            var apiRoute = baseUrl + dataModel.customerId;
            var deleteCustomer = CrudService.delete(apiRoute);
            deleteCustomer.then(function (response) {
                if (response.data != "") {
                    alert("Data Delete Successfully");
                    $scope.GetCustomers();
                    $scope.Clear();

                } else {
                    alert("Some error");
                }

            }, function (error) {
                console.log("Error: " + error);
            });
        }

        $scope.Clear = function () {
            $scope.name = "";
            $scope.address = "";
            $scope.age = "";
            $scope.email = "";
            $scope.phone = "";
            $scope.customerId = "";
            $scope.btnText = "Save";
        }

    }]);