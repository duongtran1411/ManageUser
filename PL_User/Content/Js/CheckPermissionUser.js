function FetchPermission() {
    $.ajax({
        url: "/User/GetPermissionUser",
        type: "GET",
        success: function (response) {
            if (response.success && response.permission) {
                var listPermission = JSON.stringify(response.permission)
                window.localStorage.setItem("permissions", listPermission);
            }
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}
FetchPermission();


function CheckPermission(role) {
    var permissions = window.localStorage.getItem('permissions');
    if (permissions) {
        var permissionData = JSON.parse(permissions);
        var isPermission = permissionData.find(u => u === role);
        if (isPermission) return true;
        return false;
    }
    return false;
}