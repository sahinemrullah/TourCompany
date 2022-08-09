<template>
  <v-row>
    <v-col>
      <v-data-table
        :headers="headers"
        :items="tours"
        sort-by="tourID"
        item-key="tourID"
        :show-select="showSelect"
        :single-select="singleSelect"
        :value="[tour]"
        show-expand
        :single-expand="true"
        :expanded.sync="expanded"
        :server-items-length="tourCount"
        :loading="loading"
        :options.sync="options"
        class="elevation-1"
        @update:options="options = $event"
        @input="$emit('on-select', $event)"
      >
        <template #[`top`]>
          <v-toolbar flat>
            <v-toolbar-title>{{ title }}</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-dialog v-model="dialog">
              <template #[`activator`]="{ on, attrs }">
                <v-btn
                  color="primary"
                  dark
                  class="mb-2"
                  v-bind="attrs"
                  v-on="on"
                >
                  Add Tour
                </v-btn>
              </template>
              <v-card>
                <tour-form
                  :form-title="formTitle"
                  :name="editedItem.name"
                  :destinations="editedItem.destinations"
                  @update:name="editedItem.name = $event"
                  @update:destinations="
                    editedItem.destinations = $event
                    editedItem.totalPrice = $event.reduce(
                      (sum, e) => (sum = parseInt(sum) + parseInt(e.price)),
                      0
                    )
                  "
                  @on-close="dialog = false"
                  @on-save="onSave"
                >
                </tour-form>
              </v-card>
            </v-dialog>
            <v-dialog v-model="dialogDelete" max-width="500px">
              <v-card>
                <v-card-title class="text-center"
                  >Bu turu silmek istediğinize emin misiniz?</v-card-title
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
        <template #[`expanded-item`]="{ headers, item }">
          <td :colspan="headers.length">
            <v-container>
              <v-row class="my-3">
                <v-simple-table>
                  <template #default>
                    <thead>
                      <tr>
                        <th>Name</th>
                        <th>Price(₺)</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr
                        v-for="destination in item.destinations"
                        :key="destination.destinationID"
                        class="mb-3"
                      >
                        <td>{{ destination.name }}</td>
                        <td>{{ destination.price }} ₺</td>
                      </tr>
                      <tr>
                        <td>Total Price:</td>
                        <td>
                          {{
                            item.destinations.reduce(
                              (sum, e) =>
                                (sum = parseInt(sum) + parseInt(e.price)),
                              0
                            )
                          }}
                          ₺
                        </td>
                      </tr>
                    </tbody>
                  </template>
                </v-simple-table>
              </v-row>
            </v-container>
          </td>
        </template>
        <template #[`item.totalPrice`]="{ item }">
          {{ item.destinations.reduce((sum, e) => (sum = parseInt(sum) + parseInt(e.price)), 0) }} ₺
        </template>
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
import TourForm from '~/components/Tours/TourForm.vue'
export default {
  name: 'ToursPage',
  components: { TourForm },
  props: {
    tour: {
      type: Object,
      default: () => {},
    },
    showSelect: {
      type: Boolean,
      default: false,
    },
    singleSelect: {
      type: Boolean,
      default: true,
    }
  },
  data() {
    return {
      loading: true,
      options: {
        page: 1,
        itemsPerPage: 10,
      },
      title: 'Tours',
      expanded: [],
      headers: [
        { text: 'Tour No', value: 'tourID' },
        { text: 'Name', value: 'name' },
        { text: 'Price', value: 'totalPrice' },
        { text: 'Actions', value: 'actions', sortable: false },
      ],
      editedIndex: -1,
      editedItem: {
        tourID: -1,
        name: '',
        destinations: [],
        totalPrice: 0,
      },
      dialog: false,
      dialogDelete: false,
      defaultItem: {
        tourID: -1,
        name: '',
        destinations: [],
        totalPrice: 0,
      },
      errorMessage: '',
      errorSnackBar: false,
    }
  },
  computed: {
    ...mapGetters({
      tours: 'tours/getTours',
      tourCount: 'tours/getTourCount',
    }),
    formTitle() {
      return this.editedIndex === -1 ? 'New Tour' : 'Edit Tour'
    },
  },
  watch: {
    dialog(val) {
      val || this.close()
    },
    options: {
      handler() {
        this.loading = true
        this.getTours(this.options).then((data) => {
          this.loading = false
        })
      },
      deep: true,
    },
  },
  methods: {
    ...mapActions({
      addTour: 'tours/addTour',
      getTours: 'tours/getTours',
      editTour: 'tours/editTour',
      deleteTour: 'tours/deleteTour'
    }),
    editItem(item) {
      this.editedIndex = this.tours.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },
    deleteItem(item) {
      this.editedIndex = this.tours.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialogDelete = true
    },
    deleteItemConfirm() {
      this.deleteTour({ tour: this.editedItem, idx: this.editedIndex }).then().catch(err => {
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
        this.editedIndex === -1
          ? this.addTour(this.editedItem)
          : this.editTour({tour:this.editedItem, idx: this.editedIndex})
        this.dialog = false
      }
    },
  },
}
</script>

<style>
</style>