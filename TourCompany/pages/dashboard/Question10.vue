<template>
  <v-card>
    <v-card-title>
      <v-select
        v-model="selectedDestination"
        label="Bölge"
        :items="destinations"
        item-text="name"
        item-value="name"
      >
      </v-select>
    </v-card-title>
    <v-card>
      <v-card-title> Rehber: {{ item.guide }} </v-card-title>
      <v-data-table
        :headers="headers"
        :items="item.tourists"
        :loading="loading"
        class="elevation-1"
      >
      </v-data-table>
    </v-card>
  </v-card>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
export default {
  name: 'Question7Page',
  data() {
    return {
      headers: [
        { text: 'Turist No', value: 'touristID' },
        { text: 'Turist', value: 'tourist' },
      ],
      item: {
        tourists: [],
      },
      selectedDestination: 'Dolmabahçe Sarayı',
      loading: true,
    }
  },
  async fetch() {
    this.item = await this.$axios.$get('/dashboard/GetLastBooking')
    this.loading = false
  },
  computed: {
    ...mapGetters({
      destinations: 'destinations/getDestinations',
    }),
  },
  watch: {
    selectedDestination() {
      this.loading = true
      this.$axios
        .$get(
          `/dashboard/GetLastBooking?destinationName=${this.selectedDestination}`
        )
        .then((response) => {
          this.item = response
          this.loading = false
        })
    },
  },
  created() {
    this.getDestinations({ page: 1, itemsPerPage: 999 })
  },
  methods: {
    ...mapActions({
      getDestinations: 'destinations/getDestinations',
    }),
  },
}
</script>

<style>
</style>