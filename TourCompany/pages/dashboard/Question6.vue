<template>
<v-card>
  <v-card-title>
    <v-text-field
    v-model="tourCount"
    label="Tur Sayısı Büyüktür = "
    type="number">
    </v-text-field>
  </v-card-title>
  <v-card 
  v-for="(item, idx) in items"
  :key="idx"
  class="mb-3">
  <v-card-title>
   Rehber: {{ item.name }} {{ item.surname }}
  </v-card-title>
  <v-data-table
    :headers="headers"
    :items="item.mostIntroducedPlaces"
    class="elevation-1"
  >
  </v-data-table>
  </v-card>
</v-card>
</template>

<script>
export default {
    name: "Question6Page",
    data() {
      return {
        headers: [
          { text: "Bölge No", value: "destinationID" },
          { text: "Bölge Adı", value: "name" },
          { text: "Ziyaret Sayısı", value: "count" },
        ],
        items: [],
        tourCount: 2,
      }
    },
    async fetch() {
      this.items = await this.$axios.$get('/dashboard/MostIntroducedPlaces')
    },
    watch: {
      tourCount() {
        this.$axios.$get(`/dashboard/MostIntroducedPlaces?tourCount=${this.tourCount}`).then(response => {
          this.items = response
        })
      }
    },
}
</script>

<style>

</style>