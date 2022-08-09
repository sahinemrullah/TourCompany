<template>
<v-card>
  <v-card-title>
    <v-select
    v-model="selectedCountry"
    label="Ülke"
    :items="countries"
    item-text="name"
    item-value="name">
    </v-select>
    <v-select
    v-model="selectedDestination"
    label="Bölge"
    :items="destinations"
    item-text="name"
    item-value="name">
    </v-select>
  </v-card-title>
  <v-data-table
    :headers="headers"
    :items="items"
    :loading="loading"
    class="elevation-1"
  >
  </v-data-table>
</v-card>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
export default {
    name: "Question4Page",
    data() {
      return {
        headers: [
          { text: "Rezervasyon No", value: "bookingID" },
          { text: "Turist No", value: "touristID" },
          { text: "Turist", value: "fullName" },
        ],
        items: [],
        selectedCountry: "United Kingdom",
        selectedDestination: "Kız Kulesi",
        loading: true,
      }
    },
    async fetch() {
      this.items = await this.$axios.$get('/dashboard/TouristsVisited')
      this.loading = false
    },
    computed: {
      ...mapGetters({
        countries: 'getCountries',
        destinations: 'destinations/getDestinations'
      })
    },
    watch: {
      selectedCountry() {
        this.loading = true
        this.$axios.$get(`/dashboard/TouristsVisited?countryName=${this.selectedCountry}&destinationName=${this.selectedDestination}`).then(response => {
          this.items = response
          this.loading = false
        })
      },
      selectedDestination() {
        this.loading = true
        this.$axios.$get(`/dashboard/TouristsVisited?countryName=${this.selectedCountry}&destinationName=${this.selectedDestination}`).then(response => {
          this.items = response
          this.loading = false
        })
      }
    },
    created() {
      this.getDestinations({page: 1, itemsPerPage: 999})
    },
    methods: {
      ...mapActions({
        getDestinations: 'destinations/getDestinations'
      })
    }
}
</script>

<style>

</style>