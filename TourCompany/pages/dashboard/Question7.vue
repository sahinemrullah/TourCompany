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
import { mapGetters } from 'vuex'
export default {
    name: "Question7Page",
    data() {
      return {
        headers: [
          { text: "Bölge No", value: "destinationID" },
          { text: "Bölge Adı", value: "name" },
          { text: "Ziyaret Sayısı", value: "count" },
        ],
        items: [],
        selectedNationality: "English",
        loading: false
      }
    },
    async fetch() {
      this.items = (await this.$axios.$get('/dashboard/MostVisitedPlacesByNationality'))
      this.loading = false
    },
    computed: {
      ...mapGetters({
        nationalities: 'getNationalities',
      })
    },
    watch: {
      selectedNationality() {
        this.loading = true
        this.$axios.$get(`/dashboard/MostVisitedPlacesByNationality?nationalityName=${this.selectedNationality}`).then(response => {
          this.items = response
          this.loading = false
        })
      },
    },
}
</script>

<style>

</style>