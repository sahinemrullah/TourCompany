<template>
  <v-card>
    <v-card-title>
      <span class="text-h5">{{ formTitle }}</span>
    </v-card-title>

    <v-card-text>
      <v-container>
        <v-stepper v-model="step" vertical>
          <v-stepper-step ref="stepper1" :rules="nameRules" step="1">
            Enter tour name
            <small v-if="step1ErrorMessage">{{ step1ErrorMessage }}</small>
          </v-stepper-step>

          <v-stepper-content step="1">
            <v-card class="mb-12">
              <v-row>
                <v-col cols="3"></v-col>
                <v-col cols="6">
                  <v-text-field
                    label="Name"
                    :rules="nameRules"
                    :value="name"
                    @input="
                      tourName = $event
                      $emit('update:name', $event)
                    "
                  >
                  </v-text-field>
                </v-col>
                <v-col cols="3"></v-col>
              </v-row>
            </v-card>
            <v-btn color="primary" @click="step = 2"> Continue </v-btn>
            <v-btn text @click="$emit('on-close')"> Cancel </v-btn>
          </v-stepper-content>

          <v-stepper-step ref="stepper2" step="2" :rules="destinationRules">
            Select tour destinations.
            <small v-if="step2ErrorMessage">{{ step2ErrorMessage }}</small>
          </v-stepper-step>

          <v-stepper-content step="2">
            <destination-table
              :show-select="true"
              :selected-destinations="destinations"
              class="mb-12"
              @on-select="
                selectedDestinations = $event
                $emit('update:destinations', $event)
              "
            >
            </destination-table>
            <h4>Total Price: {{ selectedDestinations.reduce((sum, e) => sum = parseInt(sum) + parseInt(e.price), 0) }} ₺</h4>
            <v-btn
              :color="validate() ? 'primary' : 'error'"
              @click="$emit('on-save', validate())"
            >
              Submit
            </v-btn>
            <v-btn text @click="step = 1"> Previous </v-btn>
          </v-stepper-content>
        </v-stepper>
      </v-container>
    </v-card-text>
  </v-card>
</template>
<script>
import DestinationTable from '../Destinations/DestinationTable.vue'

export default {
  components: { DestinationTable },
  props: {
    formTitle: {
      type: String,
      default: 'Title',
    },
    name: {
      type: String,
      default: ''
    },
    destinations: {
      type: Array,
      default: () => []
    }
  },
  emits: ['on-save', 'on-close', 'update:destinations', 'update:name'],
  data() {
    return {
      step: 1,
      step1Valid: false,
      step1ErrorMessage: null,
      tourName: this.name,
      nameRules: [this.tourNameRequired, this.tourNameMaxLength],
      step2Valid: false,
      step2ErrorMessage: null,
      selectedDestinations: this.destinations,
      destinationRules: [this.minDestinationLimit, this.maxDestinationLimit],
      minDestination: 1,
      maxDestination: 3,
    }
  },
  watch: {
    tourName(oldTourName, newTourName) {
      this.step1Valid = this.nameRules.every((validate) => {
        const validation = validate()
        if (validation === true) return true
        this.step1ErrorMessage = validation
        return false
      })
      this.step1ErrorMessage = this.step1Valid ? null : this.step1ErrorMessage
    },
    selectedDestinations(oldSelectedDestinations, newSelectedDestinations) {
      this.step2Valid = this.destinationRules.every((validate) => {
        const validation = validate()
        if (validation === true) return true
        this.step2ErrorMessage = validation
        return false
      })
      this.step2ErrorMessage = this.step2Valid ? null : this.step2ErrorMessage
    },
  },
  created() {
      this.step1Valid = this.nameRules.every((validate) => {
        const validation = validate()
        if (validation === true) return true
        this.step1ErrorMessage = validation
        return false
      })
      this.step1ErrorMessage = this.step1Valid ? null : this.step1ErrorMessage
    
      this.step2Valid = this.destinationRules.every((validate) => {
        const validation = validate()
        if (validation === true) return true
        this.step2ErrorMessage = validation
        return false
      })
      this.step2ErrorMessage = this.step2Valid ? null : this.step2ErrorMessage
  },
  methods: {
    validate() {
      return this.step1Valid && this.step2Valid
    },
    minDestinationLimit() {
      return (
        this.selectedDestinations.length >= this.minDestination ||
        `You need to select at least ${this.minDestination} destination.`
      )
    },
    maxDestinationLimit() {
      return (
        this.selectedDestinations.length <= this.maxDestination ||
        `Number of selected destinations cannot be more than ${this.maxDestination}`
      )
    },
    tourNameRequired() {
      return !!this.tourName || 'Name field required'
    },
    tourNameMaxLength() {
      return (
        this.tourName.length < 150 ||
        'Name cannot be longer than 150 characters'
      )
    },
  },
}
</script>