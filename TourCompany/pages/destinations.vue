<template>
  <v-row>
    <v-col>
      <destination-table>

      </destination-table>
    </v-col>
  </v-row>
</template>

<script>
import DestinationTable from '~/components/Destinations/DestinationTable.vue'
export default {
    name: "DestinationsPage",
    components: { DestinationTable },
}
</script>

<style>

</style>