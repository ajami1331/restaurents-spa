import { NavItem } from "../../shared/models/nav-items.model";

export let menu: NavItem[] = [
  {
    displayName: 'Restaurants',
    iconName: 'restaurant',
    route: '/restaurants',
    roles: ['user', 'admin'],
  },
  {
    displayName: 'Dashboard',
    iconName: 'dashboard',
    route: '/dashboard',
    roles: ['restaurant_owner'],
  },
  {
    displayName: 'Reviews',
    iconName: 'book',
    route: '/reviews',
    roles: ['user', 'admin'],
  },
  {
    displayName: 'Users',
    iconName: 'people',
    route: '/users',
    roles: ['admin'],
  }
];
