'use strict';

angular.module('angularUploadApp')
    .controller('fileUploadCtrl', function ($scope, $http, $timeout, $upload) {
        $scope.upload = [];
        $scope.UploadedFiles = [];

        $scope.startUploading = function ($files) {
            //$files: an array of files selected
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    $scope.upload[index] = $upload.upload({
                        url: "http://localhost:29778/api/Files", // webapi url
                        method: "POST",
                        file: $file
                    }).progress(function (evt) {
                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully
                        var length = data.length;

                        for (var i = 0; i < length; i++) {
                            var element = data[i];
                            // Do something with element i.
                            $scope.UploadedFiles.push({ FileName: element.FileName, FilePath: element.LocalFilePath, FileLength: element.FileLength });
                        }
                    }).error(function (data, status, headers, config) {
                        console.log(data);
                    });
                })(i);
            }
        }

        $scope.abortUpload = function (index) {
            $scope.upload[index].abort();
        }
    });