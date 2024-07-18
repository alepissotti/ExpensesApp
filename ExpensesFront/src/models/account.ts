import type Permission from "./permission"

export default interface Account {
    id: string,
    firstName: string,
    lastName: string,
    userName: string,
    roleId: number,
    permissions: Permission[]
}