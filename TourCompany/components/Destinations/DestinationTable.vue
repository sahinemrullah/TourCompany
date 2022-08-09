<template>
  <v-row>
    <v-col>
      <v-data-table
        :headers="headers"
        :items="destinations"
        :single-select="singleSelect"
        :show-select="showSelect"
        :value="selectedDestinations"
        sort-by="destinationID"
        class="elevation-1"
        item-key="destinationID"
        :server-items-length="destinationCount"
        :loading="loading"
        :options.sync="options"
        @update:options="options = $event"
        @input="$emit('on-select', $event)"
      >
        <template #[`top`]>
          <v-toolbar flat>
            <v-toolbar-title>{{ title }}</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-dialog v-model="dialog" max-width="290">
              <template #[`activator`]="{ on, attrs }">
                <v-btn
                  color="primary"
                  dark
                  class="mb-2"
                  v-bind="attrs"
                  v-on="on"
                >
                  Add Destination
                </v-btn>
              </template>
              <v-card>
                <destination-form
                  :name="editedItem.name"
                  :price="editedItem.price"
                  :form-title="formTitle"
                  @update:name="editedItem.name = $event"
                  @update:price="editedItem.price = $event"
                  @on-close="dialog = false"
                  @on-save="onSave"
                >
                </destination-form>
              </v-card>
            </v-dialog>
            <v-dialog v-model="dialogDelete" max-width="500px">
              <v-card>
                <v-card-title class="text-center"
                  >Bu bölgeyi silmek istediğinize emin misiniz?</v-card-title
                >
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" @click="closeDelete"
                    >Cancel</v-btn
                  >
                  <v-btn color="blue darken-1" text @click="deleteItemConfirm"
                    >Delete</v-btn
                  >
                  <v-spacer></v-spacer>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-toolbar>
        </template>
        <template #[`item.price`]="{ item }"> {{ item.price }} ₺ </template>
        <template #[`item.actions`]="{ item }">
          <v-menu bottom left>
            <template #activator="{ on, attrs }">
              <v-btn icon v-bind="attrs" v-on="on">
                <v-icon>mdi-dots-vertical</v-icon>
              </v-btn>
            </template>

            <v-list>
              <v-list-item>
                <v-list-item-title
                  ><v-btn text color="primary" @click="editItem(item)"
                    ><v-icon>mdi-pencil</v-icon> Edit</v-btn
                  ></v-list-item-title
                >
              </v-list-item>
              <v-list-item>
                <v-list-item-title
                  ><v-btn text color="error" @click="deleteItem(item)"
                    ><v-icon>mdi-delete</v-icon> Delete</v-btn
                  ></v-list-item-title
                >
              </v-list-item>
            </v-list>
          </v-menu>
        </template>
      </v-data-table>
    </v-col>
    <v-snackbar
      v-model="errorSnackBar"
    >
      {{ errorMessage }}

      <template #:action="{ attrs }">
        <v-btn
          color="blue"
          text
          v-bind="attrs"
          @click="errorSnackBar = false"
        >
          Close
        </v-btn>
      </template>
    </v-snackbar>
  </v-row>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'
import DestinationForm from '~/components/Destinations/DestinationForm.vue'
export default {
  name: 'DestinationsPage',
  components: { DestinationForm },
  props: {
    showSelect: {
      type: Boolean,
      default: false,
    },
    singleSelect: {
      type: Boolean,
      default: false,
    },
    selectedDestinations: {
      type: Array,
      default: () => [],
    },
  },
  emits: ['on-select'],
  data() {
    return {
      loading: true,
      options: {
        page: 1,
        itemsPerPage: 10,
      },
      title: 'Destinations',
      headers: [
        { text: 'Destination No', value: 'destinationID' },
        { text: 'Name', value: 'name' },
        { text: 'Price', value: 'price' },
        { text: 'Actions', value: 'actions', sortable: false },
      ],
      editedIndex: -1,
      editedItem: {
        name: '',
        price: 0,
      },
      dialog: false,
      dialogDelete: false,
      defaultItem: {
        name: '',
        price: 0,
      },
      errorMessage: '',
      errorSnackBar: false,
    }
  },
  computed: {
    ...mapGetters({
      destinations: 'destinations/getDestinations',
      destinationCount: 'destinations/getDestinationCount',
    }),
    formTitle() {
      return this.editedIndex === -1 ? 'New Destination' : 'Edit Destination'
    },
  },
  watch: {
    dialog(val) {
      val || this.close()
    },
    dialogDelete(val) {
      val || this.closeDelete()
    },
    options: {
      handler() {
        this.loading = true
        this.getDestinations(this.options).then((data) => {
          this.loading = false
        })
      },
      deep: true,
    },
  },
  methods: {
    ...mapActions({
      addDestination: 'destinations/addDestination',
      getDestinations: 'destinations/getDestinations',
      editDestination: 'destinations/editDestination',
      deleteDestination: 'destinations/deleteDestination'
    }),
    editItem(item) {
      this.editedIndex = this.destinations.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },
    deleteItem(item) {
      this.editedIndex = this.destinations.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialogDelete = true
    },
    deleteItemConfirm() {
      this.deleteDestination({ destination: this.editedItem, idx: this.editedIndex }).then().catch(err => {
        this.errorSnackBar = true
        this.errorMessage = err
      })
      this.closeDelete()
    },

    close() {
      this.dialog = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    },

    closeDelete() {
      this.dialogDelete = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    },
    onSave(e) {
      if (e === true) {
        this.dialog = false
        this.editedIndex === -1
          ? this.addDestination(this.editedItem)
          : this.editDestination({
              destination: this.editedItem,
              idx: this.editedIndex,
            })
      }
    },
  },
}
</script>

<style>
</style>