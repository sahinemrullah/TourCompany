<template>
  <v-card>
    <v-card-title>
      <span class="text-h5">{{ formTitle }}</span>
    </v-card-title>

    <v-card-text>
      <v-container>
        <v-form ref="form">
          <v-row>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                :value="name"
                label="Name"
                :rules="nameRules"
                @input="$emit('update:name', $event)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                :value="surname"
                label="Surname"
                :rules="surnameRules"
                @input="$emit('update:surname', $event)"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-menu
              :close-on-content-click="false"
              :nudge-right="40"
              transition="scale-transition"
              offset-y
              min-width="auto"
            >
              <template #activator="{ on, attrs }">
                <v-text-field
                  :value="birthDateStr"
                  :rules="birthDateRules"
                  label="Birth Date"
                  readonly
                  prepend-icon="mdi-calendar"
                  v-bind="attrs"
                  v-on="on"
                ></v-text-field>
              </template>
              <v-date-picker
                :value="birthDateStr"
                :max="new Date().toISOString().split('T')[0]"
                @input="birthDateStr = $event;$emit('update:birthDate', $event)"
              ></v-date-picker>
            </v-menu>
          </v-row>
          <v-row>
            <v-col cols="12">
              <v-radio-group
                column
                label="Gender"
                :value="gender.genderID"
                @change="$emit('update:gender', genders.filter(g => g.genderID === $event)[0])"
              >
                <v-radio
                  v-for="item in genders"
                  :key="item.genderID"
                  :label="item.name"
                  :value="item.genderID"
                ></v-radio>
              </v-radio-group>
            </v-col>
          </v-row>
          <v-row>
            <v-select
              :items="countries"
              label="Country"
              :rules="countryRules"
              persistent-hint
              prepend-icon="mdi-translate"
              item-text="name"
              item-value="countryID"
              :value="country.countryID"
              @input="$emit('update:country', countries.filter(c => c.countryID === $event)[0])"
            >
            </v-select>
          </v-row>
          <v-row>
            <v-select
              :items="nationalities"
              label="Nationality"
              :rules="nationalityRules"
              persistent-hint
              prepend-icon="mdi-translate"
              item-text="name"
              item-value="nationalityID"
              :value="nationality"
              @input="$emit('update:nationality', nationalities.filter(n => n.nationalityID === $event)[0])"
            >
            </v-select>
          </v-row>
        </v-form>
      </v-container>
    </v-card-text>

    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="warning" text @click="$emit('on-close')"> Cancel </v-btn>
      <v-btn color="primary" @click="$emit('on-save', validate())">
        Save
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  props: {
    name: {
      type: String,
      default: '',
    },
    surname: {
      type: String,
      default: '',
    },
    telephoneNumber: {
      type: String,
      default: '',
    },
    formTitle: {
      type: String,
      required: true,
    },
    birthDate: {
      type: String,
      default: "",
    },
    gender: {
      type: Object,
      default: () => {},
    },
    country: {
      type: Object,
      default: () => {},
    },
    nationality: {
      type: Object,
      default: () => {},
    },
  },
  emits: [
    'on-close',
    'on-save',
    'update:name',
    'update:surname',
    'update:birthDate',
    'update:gender',
    'update:country',
    'update:nationality',
  ],
  data() {
    return {
      birthDateStr:this.birthDate.split('T')[0],
      nameRules: [
        (v) => !!v || 'Name field is required.',
        (v) =>
          (!!v && v.length <= 20) || 'Name cannot be longer than 20 characters',
      ],
      surnameRules: [
        (v) => !!v || 'Surname field is required.',
        (v) =>
          (!!v && v.length <= 40) ||
          'Surname cannot be longer than 40 characters',
      ],
      telephoneNumberRules: [
        (v) => !!v || 'Telephone Number field is required.',
        (v) =>
          (!!v && v.length === 10) ||
          'Telephone number field should be 10 character.',
        (v) =>
          (!!v && v.includes('.') === false) ||
          "Telephone number field can't contain ',' or '.' character.",
      ],
      birthDateRules: [
        (v) => !!v || 'Birth Date field is required.',
        (v) => !isNaN(Date.parse(v)) || "Please enter valid 'mm/dd/yyyy' format date.",
        (v) => Date.parse(v) < Date.now() || "Please enter valid date" 
      ],
      countryRules: [
        (v) => (!!v && (v > 0 || v.countryID > 0)) || 'Country field is required.',
      ],
      nationalityRules: [
        (v) => (!!v && (v > 0 || v.nationalityID > 0)) || 'Nationality field is required.',
      ],
    }
  },
  computed: {
    ...mapGetters({
      genders: 'getGenders',
      countries: 'getCountries',
      nationalities: 'getNationalities',
    }),
  },
  methods: {
    validate() {
      return this.$refs.form.validate()
    },
  },
}
</script>

<style>
</style>