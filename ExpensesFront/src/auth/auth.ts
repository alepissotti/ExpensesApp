export function isAutenticathed(): Boolean {
    return !!sessionStorage.getItem('expensesAppToken');
}