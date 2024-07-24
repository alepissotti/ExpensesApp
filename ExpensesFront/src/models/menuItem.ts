export default interface MenuItem {
    name: string,
    icon: string | null,
    permission: string | null,
    navigateTo: string
}