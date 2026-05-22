import { UserRole } from "../enums/user-role";

export interface LoggedUser {
    name: string,
    email: string,
    role: UserRole
    token: string,
}

export interface UserProfile {
  name: string;
  email: string;
}