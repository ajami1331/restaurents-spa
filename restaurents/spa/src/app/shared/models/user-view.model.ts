export interface UserViewModel {
  id: string
  createdBy: string
  createdAt: Date
  updatedBy: string
  updatedAt: Date
  displayName: string
  userName: string
  roles: string[],
  isActive: boolean,
}
