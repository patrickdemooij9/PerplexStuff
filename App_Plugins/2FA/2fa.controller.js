angular.module("umbraco").controller("twoFactorAuth.controller", function ($scope, authResource, userService) {
    $scope.code = "";
    $scope.provider = "";
    $scope.providers = [];
    $scope.step = "loading";
    $scope.didFail = false;
    $scope.errorMsg = "";

    authResource.get2FAProviders()
        .then(function (data) {
            var provider = data[0];
            $scope.provider = provider;
            authResource.send2FACode(provider)
                .then(function () {
                    $scope.step = "code";
                });
        }
        );


    $scope.validate = function (provider, code) {
        $scope.code = code;
        authResource.verify2FACode(provider, code)
            .then(function (data) {
                userService.setAuthenticationSuccessful(data);
                $scope.$parent.vm.onLogin();
            },
                function (reason) {
                    console.log("didFail", reason);
                    $scope.didFail = true;
                    $scope.errorMsg = reason.errorMsg;
                }
            );
    };
})