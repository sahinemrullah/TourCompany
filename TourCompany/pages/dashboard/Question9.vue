<template>
<v-card>
  <v-card-title>
    <v-select
    v-model="selectedNationality"
    label="Uyruk"
    :items="nationalities"
    item-text="name"
    item-value="name">
    </v-select>
    <v-select
    v-model="selectedCountry"
    label="Ülke"
    :items="countries"
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
  <template #[`item.date`]="{ item }">
    {{ item.date.split('T')[0] }}
  </template>
  </v-data-table>
</v-card>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
    name: "Question9Page",
    data() {
      return {
        headers: [
          { text: "Rezervasyon No", value: "bookingID" },
          { text: "Turist", value: "tourist" },
          { text: "Bölge Adı", value: "destinationName" },
          { text: "Tarih", value: "date" },
        ],
        items: [],
        selectedNationality: "Finnish",
        selectedCountry: "Greece",
        loading: false
      }
    },
    async fetch() {
      this.items = (await this.$axios.$get('/dashboard/VisitedPlacesByCountryAndNationality'))
      this.loading = false
    },
    computed: {
      ...mapGetters({
        nationalities: 'getNationalities',
        countries: 'getCountries',
      })
    },
    watch: {
      selectedNationality() {
        this.loading = true
        this.$axios.$get(`/dashboard/VisitedPlacesByCountryAndNationality?countryName=${this.selectedCountry}&nationalityName=${this.selectedNationality}`).then(response => {
          this.items = response
          this.loading = false
        })
      },
      selectedCountry() {
        this.loading = true
        this.$axios.$get(`/dashboard/VisitedPlacesByCountryAndNationality?countryName=${this.selectedCountry}&nationalityName=${this.selectedNationality}`).then(response => {
          this.items = response
          this.loading = false
        })
      }
    },
}
</script>

<style>

</style>