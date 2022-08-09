<template>
  <v-app>
    <v-navigation-drawer
      v-model="drawer"
      fixed
      app
    >
      <v-list>
        <v-list-item
          v-for="(item, i) in items"
          :key="i"
          :to="item.to"
          router
          exact
        >
          <v-list-item-action>
            <v-icon v-for="(icon, idx) in item.icons" :key="idx">{{ icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title v-text="item.title" />
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar fixed app>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
      <v-toolbar-title v-text="title" />
      <v-spacer />
    </v-app-bar>
    <v-main>
      <v-container>
        <Nuxt />
      </v-container>
    </v-main>
    <v-footer app>
      <span>&copy; {{ new Date().getFullYear() }}</span>
    </v-footer>
  </v-app>
</template>

<script>
export default {
  name: 'DefaultLayout',
  data() {
    return {
      drawer: false,
      items: [
        {
          icons: ['mdi-apps'],
          title: 'Dashboard',
          to: '/',
        },
        {
          icons: ['mdi-card-account-details'],
          title: 'Tourists',
          to: '/tourists',
        },
        {
          icons: ['mdi-map'],
          title: 'Destinations',
          to: '/destinations',
        },
        {
          icons: ['mdi-bus-marker'],
          title: 'Tours',
          to: '/tours',
        },
        {
          icons: ['mdi-account-tie', 'mdi-account-tie-woman'],
          title: 'Guides',
          to: '/guides',
        },
        {
          icons: ['mdi-calendar-clock'],
          title: 'Bookings',
          to: '/bookings',
        },
        {
          icons: ['mdi-receipt-text-plus'],
          title: 'Invoices',
          to: '/invoices',
        },
      ],
      title: 'Tour Company',
    }
  },
}
</script>
