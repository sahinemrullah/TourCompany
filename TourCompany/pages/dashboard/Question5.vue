<template>
<v-container>
  <v-text-field
  v-model="year"
  type="number">
  </v-text-field>
  <v-data-table
    :headers="headers"
    :items="items"
    class="elevation-1"
  >
  </v-data-table>
</v-container>
</template>

<script>
export default {
    name: "Question5Page",
    data() {
      return {
        year: 2022,
        headers: [
          { text: "Bölge No", value: "destinationID" },
          { text: "Bölge Adı", value: "name" },
          { text: "Ziyaret Sayısı", value: "visitedCount" },
        ],
        items: []
      }
    },
    async fetch() {
      await this.fetchItems();
    },
    watch: {
      year(val) {
        this.fetchItems();
      }
    },
    methods: {
      fetchItems() {
        this.$axios.$get('/dashboard/VisitedPlacesSummaryBy?year='+this.year).then(response => {this.items = response});
      }
    },
}
</script>

<style>

</style>