function CheckPermission(role) {
    var permissions = window.localStorage.getItem('permissions');
    if (permissions) {
        var permissionData = JSON.parse(permissions);
        var isPermission = permissionData.find(u => u.Code === role);
        if (isPermission) return true;
        return false;
    }
    return false;
}