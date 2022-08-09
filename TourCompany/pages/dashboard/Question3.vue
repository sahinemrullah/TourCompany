<template>
<v-card>
  <v-card-title>
    <v-select
    v-model="selectedGender"
    label="Cinsiyet"
    :items="genders"
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
    name: "Question3Page",
    data() {
      return {
        headers: [
          { text: "Bölge No", value: "destinationID" },
          { text: "Bölge Adı", value: "name" },
          { text: "Ziyaret Sayısı", value: "count" },
        ],
        items: [],
        selectedGender: "Kadın",
        loading: true,
      }
    },
    async fetch() {
      this.items = await this.$axios.$get('/dashboard/totalVisitCountByGender')
      this.loading = false;
    },
    computed: {
      ...mapGetters({
        genders: 'getGenders'
      })
    },
    watch: {
      selectedGender() {
        this.loading = true
        this.$axios.$get(`/dashboard/totalVisitCountByGender?genderName=${this.selectedGender}`).then(response => {
          this.items = response
          this.loading = false
        })
      }
    },
}
</script>

<style>

</style>