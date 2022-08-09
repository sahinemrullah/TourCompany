<template>
  <v-row>
    <v-col>
      <v-data-table
        :headers="headers"
        :items="tourists"
        item-key="touristID"
        sort-by="touristID"
        :show-select="showSelect"
        :single-select="singleSelect"
        :value="selectedTourists"
        :server-items-length="touristCount"
        :loading="loading"
        :options.sync="options"
        class="elevation-1"
        @update:options="options = $event"
        @input="$emit('on-select', $event)"
      >
        <template #[`top`]>
          <v-toolbar flat>
            <v-toolbar-title>Tourists</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-dialog v-model="dialog" max-width="500px">
              <template #[`activator`]="{ on, attrs }">
                <v-btn
                  color="primary"
                  dark
                  class="mb-2"
                  v-bind="attrs"
                  v-on="on"
                >
                  Add Tourist
                </v-btn>
              </template>
              <tourist-form
                :name="editedItem.name"
                :surname="editedItem.surname"
                :gender="editedIndex === -1 ? genders[0] : editedItem.gender"
                :birth-date="editedItem.birthDate"
                :country="editedItem.country"
                :nationality="editedItem.nationality"
                :form-title="formTitle"
                @on-save="save"
                @on-close="close"
                @update:name="editedItem.name = $event"
                @update:surname="editedItem.surname = $event"
                @update:birthDate="
                  editedItem.birthDate = new Date($event)
                    .toISOString()
                    .split('T')[0]
                "
                @update:gender="editedItem.genderID = $event.genderID; editedItem.gender = $event"
                @update:country="editedItem.countryID = $event.countryID; editedItem.country = $event"
                @update:nationality="editedItem.nationalityID = $event.nationalityID; editedItem.nationality = $event"
              >
              </tourist-form>
            </v-dialog>
            <v-dialog v-model="dialogDelete" max-width="500px">
              <v-card>
                <v-card-title class="text-center"
                  >Bu rehberi silmek istediğinize emin misiniz?</v-card-title
                >
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" text @click="closeDelete"
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
        <template #[`item.birthDate`]="{ item }">
          <span>{{ item.birthDate.split('T')[0] }}</span>
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
                    ><v-icon>mdi-account-edit</v-icon> Edit</v-btn
                  ></v-list-item-title
                >
              </v-list-item>
              <v-list-item>
                <v-list-item-title
                  ><v-btn text color="error" @click="deleteItem(item)"
                    ><v-icon>mdi-account-cancel</v-icon> Delete</v-btn
                  ></v-list-item-title
                >
              </v-list-item>
            </v-list>
          </v-menu>
        </template>
      </v-data-table>
    </v-col>
  </v-row>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'
import TouristForm from '~/components/Tourists/TouristForm.vue'
export default {
  name: 'TouristsPage',
  components: { TouristForm },
  props: {
    selectedTourists: {
      type: Array,
      default: () => [],
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
      dialog: false,
      dialogDelete: false,
      headers: [
        { text: 'No', value: 'touristID' },
        { text: 'Name', value: 'name' },
        { text: 'Surname', value: 'surname' },
        { text: 'Gender', value: 'gender.name' },
        { text: 'BirthDate', value: 'birthDate', sort: (a, b) => Date.parse(a) - Date.parse(b)},
        { text: 'Nationality', value: 'nationality.name' },
        { text: 'Country', value: 'country.name' },
        { text: 'Actions', value: 'actions', sortable: false },
      ],
      editedIndex: -1,
      editedItem: {
        touristID: 0,
        name: '',
        surname: '',
        birthDate: '',
        genderID: 0,
        gender: {},
        countryID: 0,
        country: {},
        nationalityID: 0,
        nationality: {}
      },
      defaultItem: {
        touristID: 0,
        name: '',
        surname: '',
        birthDate: '',
        genderID: 0,
        gender: {},
        countryID: 0,
        country: {},
        nationalityID: 0,
        nationality: {}
      },
    }
  },
  computed: {
    ...mapGetters({
      genders: 'getGenders',
      tourists: 'tourists/getTourists',
      touristCount: 'tourists/getTouristCount',
    }),
    formTitle() {
      return this.editedIndex === -1 ? 'New Tourist' : 'Edit Tourist'
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
        this.getTourists(this.options).then((data) => {
          this.loading = false
        })
      },
      deep: true,
    },
  },
  methods: {
    ...mapActions({
      postTourist: 'tourists/postTourist',
      getTourists: 'tourists/getTourists',
      editTourist: 'tourists/editTourist',
    }),
    editItem(item) {
      this.editedIndex = this.tourists.indexOf(item)
      this.editedItem = {
        touristID: item.touristID,
        name: item.name,
        surname: item.surname,
        birthDate: item.birthDate,
        gender: item.gender,
        genderID: item.gender.genderID,
        country: item.country,
        countryID: item.country.countryID,
        nationality: item.nationality,
        nationalityID: item.nationality.nationalityID,
      }
      this.dialog = true
    },

    deleteItem(item) {
      this.editedIndex = this.tourists.indexOf(item)
      this.editedItem = {
        touristID: item.touristID,
        name: item.name,
        surname: item.surname,
        birthDate: item.birthDate,
        gender: item.gender,
        genderID: item.gender.genderID,
        country: item.country,
        countryID: item.country.countryID,
        nationality: item.nationality,
        nationalityID: item.nationality.nationalityID,
      }
      this.dialogDelete = true
    },

    deleteItemConfirm() {
      this.tourists.splice(this.editedItem, this.editedIndex)
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
    save(e) {
      if (e === true) {
        if (this.editedItem.genderID === 0)
        {
          this.editedItem.genderID = this.genders[0].genderID
          this.editedItem.gender = this.genders[0]
        }
        if (this.editedItem.countryID === 0)
        {
          this.editedItem.countryID = this.countries[0].countryID
          this.editedItem.country = this.countries[0]
        }
        if (this.editedItem.nationalityID === 0)
        {
          this.editedItem.nationalityID = this.nationalities[0].nationalityID
          this.editedItem.nationality = this.nationalities[0]
        }
        if (this.editedIndex > -1) {
          this.editTourist({ tourist: this.editedItem, idx: this.editedIndex})
        } else {
          this.postTourist(this.editedItem)
        }
        this.close()
      }
    },
  },
}
</script>

<style>
</style>